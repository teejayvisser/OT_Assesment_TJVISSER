using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Options;
using static OT.Assessment.Consumer.Models.MessageBroker;
using Newtonsoft.Json;

namespace OT.Assesment.Shared.RabbitMq
{
    public class RabbitMQService<T> : IRabbitMQService<T>
    {
        private readonly RabbitMQSetting _rabbitMqSetting;

        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IChannel _channel;


        public RabbitMQService(IOptions<RabbitMQSetting> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };
            
            _connection = _factory.CreateConnectionAsync().Result;
            _channel =  _connection.CreateChannelAsync().Result;
        }

        public async Task PublishMessageAsync(T message, string queueName)
        {
           await _channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
        }
    }
}