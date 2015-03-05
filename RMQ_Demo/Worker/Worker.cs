using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
namespace Worker
{
    class Worker
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", true, false, false, null);
                    
                    /* Fair: Dispatch 
                     * Trường hợp nếu có tin nhắn tồn đọng trong queue, nếu ta mở 2 worker lên thì workder nào 
                     * được mở trước sẻ được gửi tất cả các queue tồn đọng và cố gắng nhận trong khi worker thứ 2 
                     * không làm gì cả, vì RabbitMQ chỉ dispatch tin nhắn khi mới vào queue
                     *  [x] Để tránh trường hợp này ta chỉnh cho channel BasicQos có prefetchCount = 1, nghĩa là 
                     *      không gửi quá 1 tin nhắn cho worker này cho đến khi nó xong tin cũ và ack lên server.
                     */

                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);

                    // act = acknowledgments = false;
                    // dùng nếu trường hợp gói tin nặng mà tắt giửa chừng thì file không bị mất
                    channel.BasicConsume("task_queue", false, consumer);

                    Console.WriteLine(" [*] Waiting for messages. " +
                                      "To exit press CTRL+C");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");

                        // Nếu đã chỉnh Ack = false thì sau khi tải xong phải gọi hàm này để Rmq biết mà xóa file.
                        channel.BasicAck(ea.DeliveryTag, false); 
                    }
                }
            }
        }
    }
}
