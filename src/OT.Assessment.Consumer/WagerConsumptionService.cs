using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Repositories.WagerRepository;
using OT.Assessment.Consumer.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OT.Assessment.Consumer
{
    public class WagerConsumptionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWagerRepository _wagerRepository;

        private readonly ILogger<WagerConsumptionService> _logger;
        private readonly MessageBroker.RabbitMQSetting _rabbitMqSetting;
        private IConnection _connection;
        private IChannel _channel;

        public WagerConsumptionService(IOptions<MessageBroker.RabbitMQSetting> rabbitMqSetting,
            IServiceProvider serviceProvider, ILogger<WagerConsumptionService> logger, IWagerRepository wagerRepository)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _wagerRepository = wagerRepository;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartConsuming(MessageBroker.RabbitMQQueues.WagerQue, stoppingToken);
            await Task.CompletedTask;
        }

        private void StartConsuming(string queueName, CancellationToken cancellationToken)
        {
            _channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                bool processedSuccessfully = false;
                try
                {
                    processedSuccessfully = await ProcessMessageAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred while processing message from queue {queueName}: {ex}");
                }

                if (processedSuccessfully)
                {
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    await _channel.BasicRejectAsync(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }

        private async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // add to db 
                    var wager = JsonConvert.DeserializeObject<CasinoWagerDto>(message);
                    await _wagerRepository.ConsumeWager(wager);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
                return false;
            }
        }

        public override void Dispose()
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
            base.Dispose();
        }
    }
}