using RabbitMQ.Client;
using System.Text;

namespace Proffy.UserMicroservice.Application.Services
{
    public static class MessageService
    {
        public static void SendEmail(string email)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "emailMessage",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = email;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "emailMessage",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
