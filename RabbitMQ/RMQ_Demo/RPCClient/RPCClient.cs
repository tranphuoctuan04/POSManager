using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace RPCClient
{
    class RPCClient
    {
        IConnection connection;
        IModel channel;
        string replyQueueName;
        QueueingBasicConsumer consumer;
        public RPCClient()
        {
            // Tạo connection
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare(); 
            consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(replyQueueName, true, consumer);
        }

        public string Call(string message)
        {
            /*Tạo ra một corrId không trùng*/
            var corrId = Guid.NewGuid().ToString();
            var props = channel.CreateBasicProperties();
            props.ReplyTo = replyQueueName;
            /*Gửi thông tin này theo để lát server gửi lại sẻ kèm Id này, sau đó ta tiền hành kiểm tra, 
             nếu trùng nhau nghĩa là tin nhắn trả đúng*/
            props.CorrelationId = corrId; 

            var messageBytes = Encoding.UTF8.GetBytes(message);
            /*Gửi lên cho rpc_queue*/
            channel.BasicPublish("", "rpc_queue", props, messageBytes); 

            while (true)
            {
                // Đứng nhận tin nhắn
                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                /* Kiểm tra xem cái Id nhận lại có trùng với Id ta gửi đi hay không? */
                if (ea.BasicProperties.CorrelationId == corrId)
                {
                    return Encoding.UTF8.GetString(ea.Body);
                }
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
    class MainS
    {
        public static void Main()
        {
            var rpcClient = new RPCClient();

            Console.WriteLine(" [x] Enter number: ");
            string requestNumber = Console.ReadLine();
            var response = rpcClient.Call(requestNumber); // Gửi tin nhắn 30 đi.
            Console.WriteLine(" [.] {0}", response);

            Console.ReadLine();
            rpcClient.Close();
        }
    }
}
