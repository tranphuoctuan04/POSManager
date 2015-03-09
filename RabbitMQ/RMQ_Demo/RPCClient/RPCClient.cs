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
            replyQueueName = channel.QueueDeclare(); // Tạo queue random, có dạng VD: amq.gen-c6HCeLuk7bpCgOZWYyoujA
            consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(replyQueueName, true, consumer);
        }

        public string Call(string message)
        {
            var corrId = Guid.NewGuid().ToString();
            var props = channel.CreateBasicProperties();
            props.ReplyTo = replyQueueName;
            props.CorrelationId = corrId; // Gửi cái này kèm theo để khi nhận kiểm tra lại xem có đúng hay không

            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", "rpc_queue", props, messageBytes); // GỬi lên cho queue

            while (true)
            {
                // Đứng nhận tin nhắn
                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                // Kiểm tra xem cái Id nhận lại có trùng với Id ta gửi đi hay không?
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

            Console.WriteLine(" [x] Requesting fib(30)");
            var response = rpcClient.Call("30"); // Gửi tin nhắn 30 đi.
            Console.WriteLine(" [.] {0}", response);

            Console.ReadLine();
            rpcClient.Close();
        }
    }
}
