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
                    // Tạo queue vì có thể C sẻ chạy trước P, nếu không có queue sẻ báo lỗi.
                    channel.QueueDeclare("SomeQueue", false, false, false, null);

                    /* [x] Ta sẻ kêu server chuyển tin nhắn từ trên server xuống. Tin nhắn được chuyển
                     * từ server xuống sẻ có dạng bất động bộ, nên ta sẻ cung cấp một callback dùng làm
                     * bộ đệm để trử tin nhắn cho đến khi cần dùng đến, có là tác dụng của QueueingBasicConsumer*/
                    var consumer = new QueueingBasicConsumer(channel);

                    /* [1]: Tên của queue mà consumer sẻ nhận tin nhắn từ server.
                     * [2]: noAck: true nghĩa là server sẻ xóa tin nhắn ngay khi gửi cho C mà không
                     * cần biết C có nhận được tin nhắn đó hay không, Ack = Acknowledged.
                     * [3]: 
                     */
                    channel.BasicConsume("SomeQueue", true, consumer);

                    Console.WriteLine("[*] Waiting for messages." +
                                         "To exit press CTRL+C");
                    while (true)
                    {
                        /* Code sẻ đứng tại đây cho đến khi nhận được tin nhắn nào đó từ server*/
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        /*Tin nhắn nhận được từ server gồm các thông số khác như thuộc tính, header,..
                         tuy nhiên khi sender gửi ta không có khao báo thuộc tính, chỉ đơn thuần là gửi tin 
                         nhắn nên chỉ cần lấy tin nhắn (body)*/
                        var body = ea.Body;

                        /*Chuyển từ byte thành string và tiến hành in ra màn hình console.*/
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    }
                }
            }
        }
    }
}