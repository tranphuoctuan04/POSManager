using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;

namespace Receiveer
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
                    // Tạo queue vì có thể receiver sẻ chạy trước sender, nếu không có queue sẻ báo lỗi.
                    channel.QueueDeclare("SomeQueue", false, false, false, null);

                    // Consuming  ~ receiving
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("Hello", true, consumer);

                    Console.Write("[*] Waiting for messages." +
                                         "To exit press CTRL+C");
                    while (true)
                    {
                        // Block cho đến khi nhận được tin nhắn từ server
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    }
                }
            }
        }
    }
}
