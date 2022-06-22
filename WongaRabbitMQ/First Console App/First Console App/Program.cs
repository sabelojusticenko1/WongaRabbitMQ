using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace First_Console_App

{
    static class Program
    {
        static void Main(string[] args)
        {
            Message message = new Message();
            message.publishMsg();
        }
        }

    }

