using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.IO;

namespace Sender
{
    class Sender
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("direct_try01", "direct");
                    
                    Console.Write("Enter user: ");
                    string user = Console.ReadLine();

                    Console.Write("Enter file path: ");
                    string message = Console.ReadLine();

                    var body = File.ReadAllBytes(message);

                    var props = channel.CreateBasicProperties();
                    props.Type = message; // Dùng type = tên byte, không nên.


                    channel.BasicPublish("direct_try01", user, props, body);
                }
            }
        }
    }
}
