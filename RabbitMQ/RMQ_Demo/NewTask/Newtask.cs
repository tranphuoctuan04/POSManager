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
                     *  [x] Tiếp theo ta phải SetPersistent n = true.
                     */

                    channel.QueueDeclare("task_queue",true, false, false, null); // durable = true

                    // Tạo ra thuộc tính cơ bản của channel
                    var properties = channel.CreateBasicProperties();
                    /* Chỉnh DeliveryMode thành Persistent 
                     [x] Khi tạo queue nếu queue có durable = true thì queue sẻ không bị mất nếu server 
                     tắt, tuy nhiên tin nhắn sẻ vẫn bị mất đối với những tin nhắn thường, cho nên ta dùng 
                     hàm bên dưới để thay đổi cách channel sẻ gửi tin nhắn lên queue*/
                    properties.SetPersistent(true);

                    while (true)
                    {
                        string message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        /*Khi gửi tin nhắn, ta gửi kèm properties lên*/
                        channel.BasicPublish("", "task_queue", properties, body);
                    }
                }
            }
        }
    }
}
