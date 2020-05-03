using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.Infra.Transversal.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cadena.Importacion.QueueProcessFiles.Domain.EventHandlers
{
    public class FileProcessEventHandler : IEventHandler<FileProcessCreateEvent>
    {
        public Task Handle(FileProcessCreateEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
