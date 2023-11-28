using RabbitMQ.Client;
using System;
using System.Text;

namespace GithubRabiitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://gutmlmcm:GbjGEeM7EW_RSohaPutl8yAfayG0jWnN@toad.rmq.cloudamqp.com/gutmlmcm");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare("hello-queue", true, false, false);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string msg = $"Message {x}";

                var msgBody = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish(string.Empty, "hello-queue", null, msgBody);

                Console.WriteLine($"Message has been sent: {msg}");

            });

            Console.ReadLine();
        }
    }
}