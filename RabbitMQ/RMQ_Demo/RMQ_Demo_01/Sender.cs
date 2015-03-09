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
            // Tiến hành kết nối đến server, ở đây là localhost 
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                // Tiến hành tạo một channel, phần lớn các lệnh trong API sẻ dùng trên channel
                using (var channel = connection.CreateModel()) 
                {
                    // Để có thể gửi tin nhắn thì ta sẻ tạo một queue để gửi, tên queue ở đây là SomeQueue
                    /* [x] 1: tên của queue
                     * [x] 2: "durable": Nghĩa là cái queue này sẻ không bị mất nếu server
                     * bị tắt hoặc khởi động lại, và khôi phục lại tin nhắn, tuy nhiên, chỉ tin nhắn nào có
                     * gắn nhãn là presistent mới được khôi phục.
                     * [x] 3: "exclusive": Chỉ được dùng bởi một connection và sẻ bị xóa nếu kết
                     * nối đó đóng.
                     * [x] 4: autoDelete: Queue này sẻ tự động được delete khi mà không còn
                     * consumer nào kết nối đến nữa
                     * [x] 5: ;Argument some brokers use it to implement additional features like message TTL;
                     */
                    channel.QueueDeclare("SomeQueue", false, false, false, null);

                    while (true)
                    {
                        Console.Write(" [x] Enter Message: ");
                        string message = Console.ReadLine();

                        // Chuyển tin nhánh thành mãng byte
                        var body = Encoding.UTF8.GetBytes(message);

                        /* Đưa tin nhắn nào vào queue có tên là SomeQueue
                         * [x]1: Exchange tạm thời chưa cần đến.
                         * [x]2: tên của queue, ta đã khai báo bên trên là SomeQueue
                         * [x]3: Các thược tính của queue này tạm thời tin nhắn không có gì
                         * đặc biệt nên đễ là null.
                         * [x]4: là dữ liệu sẻ được gửi lên queue.
                         */ 
                        channel.BasicPublish("", "SomeQueue", null, body);
                    }
                }
            }
        }
    }
}
