using Cadena.Importacion.Domain.Core.Events;
using System.Threading.Tasks;

namespace Cadena.Importacion.Domain.Core.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}
