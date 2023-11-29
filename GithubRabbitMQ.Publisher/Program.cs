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

            channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers);

            Dictionary<string, object> headers = new Dictionary<string, object>
            {
                { "format", "pdf" },
                { "shape", "a4" }
            };

            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;
            channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header message"));

            Console.WriteLine("Message has been sent");

            Console.ReadLine();
        }
    }

    public enum LogNames
    {
        Critial = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
}