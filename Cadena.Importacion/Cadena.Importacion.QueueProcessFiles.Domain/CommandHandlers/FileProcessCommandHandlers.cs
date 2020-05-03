using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.Infra.Transversal.Events;
using Cadena.Importacion.QueueProcessFiles.Domain.Commands;
using MediatR;
using System.IO;
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
            for (int i = 0; i < request.JsonFilesConfiguration.Count; i++)
            {
                _bus.Publish(new FileProcessCreateEvent(request.Guid, request.Date, request.JsonFilesConfiguration[i]));
                File.Delete(request.FilesImported[i]);
            }

            return Task.FromResult(true);
        }
    }
}
