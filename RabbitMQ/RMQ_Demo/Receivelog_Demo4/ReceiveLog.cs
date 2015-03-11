using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ReceiveLogs
{
    class ReceiveLog
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    /**/
                    channel.ExchangeDeclare("direct_logs", "direct");

                    var queueName = channel.QueueDeclare().QueueName;

                    Console.WriteLine("Enter severity (severity,severity,...)");
                    string severity = Console.ReadLine();

                    /*Tách chuỗi theo dấu , nghĩa là consumer này sẻ nói với exchange là chỉ nhận những tin nhắn
                     có routing key là những chuỗi nhập vào, cách nhau bởi dấu ,*/
                    foreach (string s in severity.Split(','))
                    {
                        channel.QueueBind(queueName, "direct_logs", s.Trim());
                    }

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, true, consumer);

                    Console.WriteLine("[*] Waiting for Message");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] {0}", message);
                    }
                }
            }
        }
    }
}
