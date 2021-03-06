﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RPCServer
{
    class RPCServer
    {
        private static int fib(int n)
        {
            if (n == 0 || n == 1) return n;
            return fib(n - 1) + fib(n - 2);
        }

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    /*Tạo queue, tất cả request sẻ gửi lên queue này*/
                    channel.QueueDeclare("rpc_queue",false, false, false, null);

                    /*Không gửi quá một tin nhắn cho Worker này*/
                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("rpc_queue", false, consumer);

                    Console.WriteLine(" [x] Awaiting RPC request");
                    while (true)
                    {
                        string response = null;

                        /* Đợi bắt tin nhắn từ queue */
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        
                        var body = ea.Body; // Lấy nội dung tin nhắn
                        var props = ea.BasicProperties; // Lấy thiết lập của tin nhắn
                        var replyProps = channel.CreateBasicProperties(); // Tạo ra thiết lập mới của tin nhắn

                        // Gán corrId của tin gửi lên với replyProps(sẻ gửi về) để bên nhận kiểm tra Id
                        replyProps.CorrelationId = props.CorrelationId; 
                        try
                        {
                            var message = Encoding.UTF8.GetString(body);

                            int n = int.Parse(message);
                            Console.WriteLine(" [.] fib({0})", message);
                            response = fib(n).ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ex: " + e.Message);
                            response = "";
                        }
                        finally
                        {
                            //Console.WriteLine("props.Replyto: " + props.ReplyTo.ToString());
                            var responseBytes = Encoding.UTF8.GetBytes(response);
                            // props.ReplyTo là tên của queue đã tạo bên client, nói chung là để gửi cho đúng queue.
                            channel.BasicPublish("", props.ReplyTo, replyProps, responseBytes);

                            /*Do bên trên đã thiết lập Qos nên khi nhận và xử lý xongt in nhắn phải ack cho 
                             server biết để nếu còn tin nhắn tồn đọng trong queue thì server sẻ tiếp tục gửi*/
                            channel.BasicAck(ea.DeliveryTag, false);
                        }




                    }
                }
            }
        }
    }
}
