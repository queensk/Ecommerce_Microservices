using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace E_MessageBus
{
    public class MessageBus : IMessageBus
    {
        public string ConnectionString ="";

        public async Task PublishMessage(object message, string queue_topic_name)
        {
            var ServiceBus = new ServiceBusClient(this.ConnectionString);
            var sender = ServiceBus.CreateSender(queue_topic_name);
            var message_json = JsonConvert.SerializeObject(message);
            var message_body = new ServiceBusMessage(Encoding.UTF8.GetBytes(message_json))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(message_body);
            await ServiceBus.DisposeAsync();
        }
    }
}