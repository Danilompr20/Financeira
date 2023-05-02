using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessageBus
    {
        Task PublicMessgage(BaseMessage message ,string topicName);
    }
}
