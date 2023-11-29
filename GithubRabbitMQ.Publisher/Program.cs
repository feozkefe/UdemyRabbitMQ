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

            channel.ExchangeDeclare("logs-direct", durable: true, type: ExchangeType.Direct);

            foreach (LogNames logName in Enum.GetValues(typeof(LogNames)))
            {
                var routeKey = $"route-{logName}";
                var queueName = $"direct-queue-{logName}";

                channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
                channel.QueueBind(queueName, "logs-direct", routeKey, null);
            }

            var random = new Random();
            var numbers = Enumerable.Range(1, 50).ToList();

            foreach (var x in numbers)
            {
                LogNames log = (LogNames)random.Next(1, 5);

                string msg = $"log-type: {log}";

                var msgBody = Encoding.UTF8.GetBytes(msg);
                var routeKey = $"route-{log}";

                channel.BasicPublish("logs-direct", routeKey, null, msgBody);

                Console.WriteLine($"Log has been sent: {msg} to route: {routeKey}");
            }

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    string msg = $"log {x}";

            //    var msgBody = Encoding.UTF8.GetBytes(msg);

            //    channel.BasicPublish("logs-direct", "", null, msgBody);

            //    Console.WriteLine($"Message has been sent: {msg}");

            //});

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