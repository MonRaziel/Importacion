using Cadena.Importacion.Infra.Transversal.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cadena.Importacion.Infra.Transversal.Models
{
    public class LocalConfigurationSettings : ILocalConfigurationSettings
    {
        private const string _localKeys = "LocalKeys";

        public LocalConfigurationSettings(IConfiguration configuration)
        {
            configuration.GetSection(_localKeys).Bind(this);
        }


        public int ExecutionTime { get; set; }
        public string PathRead { get; set; }
        public string RabbitMQUrl { get; set; }
    }
}
