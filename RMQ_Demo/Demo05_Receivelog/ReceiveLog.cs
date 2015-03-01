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
                    /* Topic exchange
                     * * = Thay thể cho một chử
                     * # = không có gì hoặc thay thế cho nhiều chữ
                     */
                    channel.ExchangeDeclare("topic_logs", "topic");

                    var queueName = channel.QueueDeclare().QueueName;

                    Console.WriteLine("Enter severity (severity,severity,...)");
                    string severity = Console.ReadLine();

                    foreach (string s in severity.Split(','))
                    {
                        /* bind vào exchange nhửng routingKey này, nếu tin  nhắn gửi lên cho exchange mà có chứa
                         * những key đã được binding thì sẻ gửi cho consummer này.
                        */
                        channel.QueueBind(queueName, "topic_logs", s.Trim());
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
