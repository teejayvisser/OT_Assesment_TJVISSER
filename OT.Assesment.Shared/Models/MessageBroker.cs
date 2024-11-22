namespace OT.Assessment.Consumer.Models;

public class MessageBroker
{
    public class RabbitMQSetting
    {
        public string? HostName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    //RabbitMQ Queue name
    public static class RabbitMQQueues
    {
        public const string WagerQue = "WagerQue";
    }
}