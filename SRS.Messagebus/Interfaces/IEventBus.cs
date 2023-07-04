using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Messagebus.Interfaces
{
    internal interface IEventBus
    {
        Task PublishAsync<TEvent>(params TEvent[] events)
           where TEvent : class;

        Task PublishAsync<TEvent>(TEvent @event, MessageProperties messageProperties, CancellationToken cancellationToken = default)
            where TEvent : class;
    }
}
