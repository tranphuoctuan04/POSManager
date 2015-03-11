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
                    /* 
                     *  [x] fanout: Gửi tin nhắn nó nhận được cho tất cả các queue nó biết.
                     */
                    channel.ExchangeDeclare("logs", "fanout");

                    while(true)
                    {
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        /*VD trước ta gửi tin nhắn vào thẳng queue, ở đây là sẻ gửi vào exchange.*/
                        channel.BasicPublish("logs", "", null, body);
                    }
                }
            }
        }
    }
}
