using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using E_EmailService.Services;
using Microsoft.VisualBasic;

namespace E_EmailService.Messaging
{
    public class AzureMessageBusConsumer : IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string QueueName;
        private readonly string topics;
        private readonly string subscription;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly ServiceBusProcessor _orderEmails;
        private readonly EmailSendService _emailService;
        private readonly EmailService _saveToDb;
        private readonly ILogger<AzureMessageBusConsumer> _logger;
        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }

    internal class EmailSendService
    {
    }
}