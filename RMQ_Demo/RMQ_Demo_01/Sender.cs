using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RMQ_Demo_01
{
    class Send
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Để có thể gửi tin nhắn thì ta sẻ tạo một queue để gửi, tên queue ở đây là Hello
                    channel.QueueDeclare("SomeQueue", false, false, false, null);

                    while (true)
                    {
                        Console.Write(" [x] Enter Message: ");
                        string message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", "Hello", null, body);
                        // Console.Write("[x] Send {0}", message);
                    }
                }
            }
        }
    }
}
