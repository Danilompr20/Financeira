using Financeira.Domain.ViewModel;
using MessageBus;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Financeira.RabbitMQSender
{
    public class RabitSender : IRabbitSender
    {
        private readonly string _hostName;
        private readonly string _passordName;
        private readonly string _userName;
        private IConnection _conection ;

        public RabitSender()
        {
            _hostName = "localhost";
            _passordName = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _passordName
            };
            _conection = factory.CreateConnection();
            using var channel = _conection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray(message);
            channel.BasicPublish(exchange:"", routingKey:queueName, basicProperties: null, body:body);
            
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            // para serializar os filhos
            var options = new JsonSerializerOptions
            {
               WriteIndented= true
            };
            var json = JsonSerializer.Serialize<ClienteViewModel>((ClienteViewModel)message);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
