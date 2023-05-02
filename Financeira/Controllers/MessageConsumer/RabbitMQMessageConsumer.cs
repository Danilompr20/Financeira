using Financeira.Domain.Entity;
using Financeira.Domain.ViewModel;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Financeira.Controllers.MessageConsumer
{
    public class RabbitMQMessageConsumer : BackgroundService
    {

        private IConnection _conection;
        private IModel _channel;

        public RabbitMQMessageConsumer()
        {
          
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _conection = factory.CreateConnection();
            _channel = _conection.CreateModel();
            _channel.QueueDeclare(queue: "clientequeue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                ClienteViewModel cliente = JsonSerializer.Deserialize<ClienteViewModel>(content);
                ProcessarCliente(cliente).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("clientequeue", false, consumer);
            
            return Task.CompletedTask;
        }

        private async Task ProcessarCliente(object cliente)
        {
            var teste = cliente;
        }
    }
}
