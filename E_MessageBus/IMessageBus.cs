using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(object message, string queue_topic_name);
    }
}