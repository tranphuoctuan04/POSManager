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
                     * Topic exchange là lọc tin nhắn theo nhiều điều kiện khác nhau.
                     *  [x] VD <Người dùng>.<Lỗi>
                     *      admin.*: Mọi lỗi mà admin bị
                     *      *.high : Lỗi mà bất cứ người nào bị có độ ưu tiên cao.
                     */
                    channel.ExchangeDeclare("topic_logs", "topic");
                    Console.WriteLine("Severity: info - warning - error");
                    Console.WriteLine("Enter Message (Message:Sender.Severity)");
                    while(true)
                    {
                        var message = Console.ReadLine();
                        if (!message.Contains(':'))
                            continue;
                        
                        string[] arr = message.Split(':');

                        var severity = arr[1];
                        var body = Encoding.UTF8.GetBytes(arr[0]);

                        // routingKey = severity, tùy vào severity mà tin nhắn này sẻ đến đúng comsumer cần đến.
                        channel.BasicPublish("topic_logs", severity, null, body);
                    }
                }
            }
        }
    }
}
