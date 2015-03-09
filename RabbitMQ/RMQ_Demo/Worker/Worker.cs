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
                    /*
                     [1]: prefectSize: Nghĩa là tin nhắn sẻ gửi trước, khi mà client đã xử lý xong tin nhắn trước
                     đó thì tin nhắn này đã được gửi trước, nên nó sẻ nằm trong máy thay vì gửi xuống channel
                     [2]: prefect-count: Nói với rabbitMQ rằng cũng một lúc không gửi quá 1 tin nhắn cho worker
                     này, mà phải đợi cho worker đó xong trước tin nhắn trước đó, chỉ hoạt động khi noAck = false;
                     */
                    channel.BasicQos(0,1, false);

                    var consumer = new QueueingBasicConsumer(channel);

                    /*ack = acknowledgments = false, nghĩa là khi gửi tin nhắn từ server thì queue đó trên 
                     server chưa mất cho ack = false, nếu đã chỉnh ack = false thì sau khi xử lý xong tin nhắn
                     phải ack lên server.*/
                    channel.BasicConsume("task_queue", false, consumer);

                    Console.WriteLine(" [*] Waiting for messages. " +
                                      "To exit press CTRL+C");
                    while (true)
                    {
                        /*Dequeue*/
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        /*Lấy nội dung tin nhắn*/
                        var body = ea.Body;
                        /*Chuyển sang string*/
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);

                        /*1 dấu . trong tin nhắn sẻ delay 1 giây, dùng để test cho nhận thấy được tin nhắn
                         được dispatch như thế nào*/
                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");

                        /*Nếu đã chỉnh Ack = false thì sau khi tải xong phải gọi hàm này để RMQ xóa và gửi tiếp nếu
                         còn file trong queue.*/
                        channel.BasicAck(ea.DeliveryTag, false); 
                    }
                }
            }
        }
    }
}
