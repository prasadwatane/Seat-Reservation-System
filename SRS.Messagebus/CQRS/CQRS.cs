using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Messagebus.CQRS
{
    internal class CQRS
    {
        public Cqrs(IEventBus eventBus,
           ICommandBus commandBus,
           IQueryFacade queryFacade)
        {
            EventBus = eventBus;
            CommandBus = commandBus;
            QueryFacade = queryFacade;
        }

        public IEventBus EventBus { get; }

        public IEnterpriseServiceBus EnterpriseServiceBus { get; }

        public ICommandBus CommandBus { get; }

        public IEnterpriseCommandBus EnterpriseCommandBus { get; }

        public IQueryFacade QueryFacade { get; }
    }
}
