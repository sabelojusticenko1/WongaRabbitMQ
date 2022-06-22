using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace First_Console_App
{
    class RabbitConnection
    {
        public IModel getChannel() {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:sabelo@localhost:5672") // rabbitMQ Uri
            };
            var connection = factory.CreateConnection(); // create the connection return i connection object (default without any parameter)
            var channel = connection.CreateModel(); // create the channel return i Model (channel created)
            return channel;
        }
    }
}
