using System;
using System.Collections.Generic;
using System.Text;

namespace Cadena.Importacion.Infra.Transversal.Interfaces
{
    public interface ILocalConfigurationSettings
    {
        public string PathRead { get; set; }
        public int ExecutionTime { get; set; }

        public string RabbitMQUrl { get; set; }
    }
}
