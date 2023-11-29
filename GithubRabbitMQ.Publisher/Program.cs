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

            channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic);

            var rnd = new Random();
            var numbers = Enumerable.Range(1, 50).ToList();

            foreach (var x in numbers)
            {
                LogNames log1 = (LogNames)rnd.Next(1, 5);
                LogNames log2 = (LogNames)rnd.Next(1, 5);
                LogNames log3 = (LogNames)rnd.Next(1, 5);
                var routeKey = $"{log1}.{log2}.{log3}";

                string msg = $"log-type: {log1}-{log2}-{log3}";
                var msgBody = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish("logs-topic", routeKey, null, msgBody);

                Console.WriteLine($"Log has been sent: {msg} to route: {routeKey}");
            }
         
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