using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.QueueProcessFiles.Application.Interfaces;
using Cadena.Importacion.QueueProcessFiles.Domain.Commands;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cadena.Importacion.QueueProcessFiles
{
    public class ReadFilesWorker : IHostedService, IDisposable
    {
        private readonly System.Timers.Timer _timer;
        //private readonly IEventBus _bus;
        private readonly IFileProcess _process;
        private static object _lock = new object();

        public ReadFilesWorker(IEventBus bus, IFileProcess process)
        {
            _process = process;
            //_bus = bus;
            _timer = new System.Timers.Timer
            {
                Interval = 30000,
                Enabled = true
            };
            _timer.Elapsed += Timer_Elapsed;
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

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.File.Create(@"D:\Borrar\Logs\logs.txt")))
                    {
                        file.WriteLine($"Executed: {DateTime.Now.ToString()}");
                    }

                    _process.Process(@"D:\Borrar\Files");

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
