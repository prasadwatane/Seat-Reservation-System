using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using MassTransit;
using MassTransit.Configuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Messagebus.Commands
{
    internal class CommandBus 
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class
        {
            await _mediator.Send(command, cancellationToken);
        }

        public async Task SendAsync<TCommand>(TCommand command, SeatViewModel messageProperties, CancellationToken cancellationToken = default)
            where TCommand : class
        {
            var pipe = Pipe.New<SendContext>(c =>
            {
                c.AddPipeSpecification(new AsyncDelegatePipeSpecification<SendContext>(s =>
                {
                    object value = s.Headers.SetContextProperties(messageProperties);
                    return Task.CompletedTask;
                }));
            });

            await _mediator.Send(command, pipe, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default, TimeSpan? timeout = null)
            where TCommand : class
            where TResponse : class
        {
            return await SendAsync<TCommand, TResponse>(command, null, cancellationToken, timeout);
        }

       public async Task<TResponse> SendAsync<TCommand, TResponse>(TCommand command, SeatViewModel messageProperties, CancellationToken cancellationToken = default, TimeSpan? timeout = null)
            where TCommand : class
            where TResponse : class
        {
            var client = _mediator.CreateRequestClient<TCommand>();
            var response = await client.GetResponse<TResponse>(command,
                c =>
                {
                    if (messageProperties != null)
                    {
                        c.AddPipeSpecification(new AsyncDelegatePipeSpecification<MassTransit.SendContext<TCommand>>(s =>
                        {
                            s.Headers.SetContextProperties(messageProperties);
                            return Task.CompletedTask;
                        }));
                    }
                },
                cancellationToken,
                timeout ?? MassTransitDefaults.Timeout);
            return response.Message;
        }
    }
}
