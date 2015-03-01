using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;
namespace Receiver
{
    class Receiver
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("direct_try01", "direct");

                    var queueName = channel.QueueDeclare().QueueName;

                    Console.Write("Enter username: ");
                    string user = Console.ReadLine();

                    channel.QueueBind(queueName,"direct_try01",user);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName,true,consumer);

                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        //string message = Encoding.UTF8.GetString(ea.Body);

                        var prop = ea.BasicProperties;

                        File.WriteAllBytes(prop.Type,ea.Body);

                        Console.WriteLine("Received: " + prop.Type);
                    }
                }
            }
        }
    }
}
