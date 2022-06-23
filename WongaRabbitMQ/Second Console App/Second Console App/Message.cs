using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Second_Console_App
{
    public class Message
    {
        private IModel channel;
        private readonly RabbitConnection _rabbitConnection;

        public Message(){
            _rabbitConnection = new RabbitConnection();
            channel = _rabbitConnection.getChannel();
            CreateQ();
        }
        public void CreateQ() {
            //declare a queue
            channel.QueueDeclare("AppTwo-queue", //pass a queue name
                durable: true, // we want the meesage to stays until the consumer recieve it
                exclusive: false, //for auto exchange
                autoDelete: false, //for auto exchange
                arguments: null);  //for auto exchange

        }

        public void consumeMsg()
        {
            var Name = default(string);
            var consumer = new EventingBasicConsumer(channel); // create a consumer 
            consumer.Received += (sender, e) =>
                    {
                        var body = e.Body.ToArray();
                        Name = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Hello my name is : " + Name);
                        publishMsg(Name);
                    };
            try
            {
                channel.BasicConsume("AppOne-queue", true, consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Publisher to create AppOne-queue");
            }
            finally
            {
                Console.ReadLine();
            }

        }

        public void publishMsg(string Name) {
            var body = Encoding.UTF8.GetBytes(Name);
            channel.BasicPublish(exchange: "",
                                     routingKey: "AppTwo-queue",
                                     basicProperties: null,
                                     body: body);
            
        }
    }
}
