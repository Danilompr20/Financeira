using MessageBus;

namespace Financeira.RabbitMQSender
{
    public interface IRabbitSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
