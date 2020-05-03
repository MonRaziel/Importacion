using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.Infra.Transversal.Events;
using Cadena.Importacion.Infra.Transversal.Interfaces;
using Cadena.Importacion.QueueProcessFiles.Application.Interfaces;
using Cadena.Importacion.QueueProcessFiles.Domain.EventHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cadena.Importacion.QueueProcessFiles
{
    public class ReadFilesWorker : IHostedService, IDisposable
    {
        private readonly System.Timers.Timer _timer;
        private readonly IFileProcess _process;
        private readonly ILocalConfigurationSettings _localConfig;

        private static object _lock = new object();

        public ReadFilesWorker(IFileProcess process, IEventBus bus, ILocalConfigurationSettings localConfig)
        {
            _process = process;
            _localConfig = localConfig;

            _timer = new System.Timers.Timer
            {
                Interval = _localConfig.ExecutionTime,
                Enabled = true
            };
            _timer.Elapsed += Timer_Elapsed;

            //Test to get message from RabbitMQ
            //bus.Subscribe<FileProcessCreateEvent, FileProcessEventHandler>();
        }

        #region Worker Methods

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer?.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Enabled = false;
            return Task.CompletedTask;
        }

        #endregion

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Monitor.TryEnter(_lock))
                {
                    _timer.Stop();

                    _process.Process(_localConfig.PathRead);

                    _timer.Start();
                }
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
