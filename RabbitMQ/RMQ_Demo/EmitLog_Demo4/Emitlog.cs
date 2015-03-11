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
                    /*Direct_logs chỉ gửi tin nhắn cho những queue nào mà có routing key phù hợp*/
                    channel.ExchangeDeclare("direct_logs", "direct");
                    Console.WriteLine("Severity: info - warning - error");
                    Console.WriteLine("Enter Message (Message:Severity)");
                    while(true)
                    {
                        /*Tin nhắn có dạng <Tin nhắn>:<Routing key>*/
                        var message = Console.ReadLine();
                        if (!message.Contains(':'))
                            continue;
                        
                        string[] arr = message.Split(':');


                        var severity = arr[1];
                        var body = Encoding.UTF8.GetBytes(arr[0]);

                        /*routingKey = severity, tùy vào severity mà tin nhắn này sẻ đến đúng comsumer cần đến.*/
                        channel.BasicPublish("direct_logs", severity, null, body);
                    }
                }
            }
        }
    }
}
