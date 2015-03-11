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
                    /*Tạo ra 1 exchange kiểu fanout: fanout là tin nhắn sẻ được gửi cho tất cả các queue đang
                     kết nối vối exchange này */
                    channel.ExchangeDeclare("logs", "fanout");

                    /*Các VD trước khi tạo queue thì ta gán cho nó một tên nhưng trong trường hợp này khi
                     kết nối với exchange ta cần một tên queue khác mà không trùng với các tên queue đã có,
                     hàm bên dưới sẻ yeu cầu server tạo một queue không trùng tên, queue bên dưới durable = false
                     Một random queue thường có dạng tương tự amq.gen-JzTY20BRgKO-HjmUJj0wLg.*/
                    var queueName = channel.QueueDeclare().QueueName;

                    /*Bind queue vừa mới tạo với Exchange*/
                    channel.QueueBind(queueName, "logs", "");

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
