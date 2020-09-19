using Proffy.EmailMicroservice.Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Proffy.EmailMicroservice.Application
{
    class Program
    {
        static void Main(string[] args)
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

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += Consumer_Received;

                channel.BasicConsume(queue: "emailMessage",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Aguardando mensagens para processamento...");
                Console.ReadKey();
            }
        }

        private static void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                EmailService.SendEmail(message);

                Console.WriteLine(Environment.NewLine +
                    "[Email enviado] " + message);
            }
            catch
            {
                Console.WriteLine(Environment.NewLine +
                "[Não foi possível enviar um e-mail] " + message);
            }
        }
    }
}
