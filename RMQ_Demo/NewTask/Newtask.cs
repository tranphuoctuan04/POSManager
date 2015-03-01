using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace NewTask
{
    class NewTask
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    /* Bên consumer đã dùng Ack nên tin nhắn sẻ không bị mất nếu consumer tắt giữa chừng,
                     * nhưng nếu server của RabbitMQ bị mất thì tin nhắn vẫn sẻ mất, vì thế bên dưới ở phần
                     *  [x] QueueDeclare ta cho durable = true ~ task_queue sẻ không bị mất nếu RabbitMQ server tắt
                     *  [x] Tiếp theo ta phải SetPersistent cho tin nhắn = true.
                     */

                    channel.QueueDeclare("task_queue",true, false, false, null); // durable = true
                    var properties = channel.CreateBasicProperties();
                    properties.SetPersistent(true);

                    while (true)
                    {
                        string message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", "task_queue", properties, body);
                       // Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            }
        }

    }
}
