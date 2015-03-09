using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace EmitLog
{
    class Emitlog
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    /* Thường RabbitMQ không gửi trực tiếp tin nhắn từ Producer lên cho queue mà thông qua trung gian
                     * gọi là exchange, exchange sẻ nhận tin nhắn từ producer và gửi lên cho queue và exchange biết 
                     * phải làm gì mỗi khi nó nhận được tin nhắn Xóa, gửi cho ai ..., có một số dạng fanout 
                     * direct, topic, headers and fanout ở đây ta sẻ dùng fanout
                     *  [x] fanout: Gửi tin nhắn nó nhận được cho tất cả các queue nó biết.
                     */
                    channel.ExchangeDeclare("logs", "fanout");

                    while(true)
                    {
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        // Thay gì trước đây gửi cho queue, giờ ta gửi cho exchange
                        channel.BasicPublish("logs", "", null, body);
                    }
                }
            }
        }
    }
}
