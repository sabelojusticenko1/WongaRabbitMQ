using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace First_Console_App
{
    public class Message
    {
        private IModel channel;
        private readonly RabbitConnection _rabbitConnection;

        public Message(){
            _rabbitConnection = new RabbitConnection();
        }

        public void publishMsg() {
            channel = _rabbitConnection.getChannel();
            //declare a queue
            channel.QueueDeclare("AppOne-queue", //pass a queue name
                durable: true, // we want the meesage to stays until the consumer recieve it
                exclusive: false, //for auto exchange
                autoDelete: false, //for auto exchange
                arguments: null);  //for auto exchange

            string Name;

                Console.WriteLine("Enter your name: "); // output or print the value
                Name = Console.ReadLine();   

            while (string.IsNullOrEmpty(Name))
            {
                Console.WriteLine("Name can't be empty! Enter your name");
                Name = Console.ReadLine();
            }
            channel = _rabbitConnection.getChannel();
                var body = Encoding.UTF8.GetBytes(Name);
                channel.BasicPublish(exchange: "",
                                         routingKey: "AppOne-queue",
                                         basicProperties: null,
                                         body: body);
                    consumeMsg();
                }
        
        public void consumeMsg()
        {
            channel = _rabbitConnection.getChannel();
            // Consumer for APP one (for ACK)
            var RecievedName = default(string);
            var consumer = new EventingBasicConsumer(channel); // create a consumer 
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                RecievedName = Encoding.UTF8.GetString(body);
                Console.WriteLine("Hello : " + RecievedName + " I am your father");

            };
            channel.BasicConsume("AppTwo-queue", true, consumer);
            Console.ReadLine();
        }
    }
}
