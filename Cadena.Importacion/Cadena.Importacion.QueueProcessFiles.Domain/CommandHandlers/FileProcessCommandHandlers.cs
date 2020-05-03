using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.Infra.Transversal.Events;
using Cadena.Importacion.QueueProcessFiles.Domain.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cadena.Importacion.QueueProcessFiles.Domain.CommandHandlers
{
    public class FileProcessCommandHandlers : IRequestHandler<CreateFileProcessCommand, bool>
    {
        private readonly IEventBus _bus;

        public FileProcessCommandHandlers(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateFileProcessCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ

            foreach (string json in request.JsonFilesConfiguration)
            {
                _bus.Publish(new FileProcessCreateEvent(request.Guid, request.Date, json));
            }

            return Task.FromResult(true);
        }
    }
}
