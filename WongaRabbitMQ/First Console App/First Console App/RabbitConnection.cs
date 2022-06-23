using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace First_Console_App
{
    class RabbitConnection
    {
        public IModel getChannel()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672") // rabbitMQ Uri
                };
                var connection = factory.CreateConnection(); // create the connection 
                var channel = connection.CreateModel(); // create the channel return i Model 
                return channel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}