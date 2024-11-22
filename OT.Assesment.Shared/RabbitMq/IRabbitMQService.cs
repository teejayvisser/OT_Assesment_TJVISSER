using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assesment.Shared.RabbitMq
{
    public interface IRabbitMQService<T>
    {
        Task PublishMessageAsync(T message, string queueName);
    }
}
