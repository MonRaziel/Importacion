using Cadena.Importacion.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cadena.Importacion.Infra.Transversal.Events
{
    public class FileProcessCreateEvent : Event
    {
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }
        public string JsonFilesConfiguration { get; set; }

        public FileProcessCreateEvent(Guid guid, DateTime date, string jsonFilesConfiguration)
        {
            Guid = guid;
            Date = date;
            JsonFilesConfiguration = jsonFilesConfiguration;
        }
    }
}
