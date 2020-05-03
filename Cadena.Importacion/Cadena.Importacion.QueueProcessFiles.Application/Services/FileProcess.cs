using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.QueueProcessFiles.Application.Interfaces;
using Cadena.Importacion.QueueProcessFiles.Domain.Commands;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cadena.Importacion.QueueProcessFiles.Application.Services
{
    public class FileProcess : IFileProcess
    {
        private readonly IEventBus _bus;
        public FileProcess(IEventBus bus)
        {
            _bus = bus;
        }

        public bool Process(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var files = Directory.GetFiles(path, "*.json");
                var lstFiles = new List<string>();
                var lstFilesImported = new List<string>();

                foreach (string file in files)
                {
                    lstFiles.Add(File.ReadAllText(file));
                    lstFilesImported.Add(file);
                }

                _bus.SendCommand(new CreateFileProcessCommand() { Guid = new Guid(), Date = DateTime.Now, JsonFilesConfiguration = lstFiles, FilesImported = lstFilesImported });
            }

            return true;
        }
    }
}
