using Cadena.Importacion.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cadena.Importacion.QueueProcessFiles.Domain.Commands
{
    public class CreateFileProcessCommand : Command
    {
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }
        public List<string> JsonFilesConfiguration { get; set; }
        public List<string> FilesImported { get; set; }
    }
}
