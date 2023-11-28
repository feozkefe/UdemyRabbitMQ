using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Channels;

namespace GithubRabiitMQ.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://gutmlmcm:GbjGEeM7EW_RSohaPutl8yAfayG0jWnN@toad.rmq.cloudamqp.com/gutmlmcm");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var randomQueueName = channel.QueueDeclare().QueueName;

            //when we dont want to delete the queue
            //var randomQueueName = "log-database-save-queue";
            //channel.QueueDeclare(randomQueueName, true, false, false);

            channel.QueueBind(randomQueueName, "logs-fanout", "", null);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(randomQueueName, false, consumer);

            Console.WriteLine("Logs are listening.");

            consumer.Received += (sender, e) => Consumer_Received(sender, e, channel);

            Console.ReadLine();
        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e, IModel channel)
        {
            var msg = Encoding.UTF8.GetString(e.Body.ToArray());

            Thread.Sleep(1500);

            Console.WriteLine($"Received Message: {msg}");

            channel.BasicAck(e.DeliveryTag, false);
        }
    }
}