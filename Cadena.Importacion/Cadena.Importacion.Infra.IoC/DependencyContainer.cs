using Cadena.Importacion.Domain.Core.Bus;
using Cadena.Importacion.Infra.Bus;
using Cadena.Importacion.Infra.Transversal.Events;
using Cadena.Importacion.Infra.Transversal.Interfaces;
using Cadena.Importacion.Infra.Transversal.Models;
using Cadena.Importacion.QueueProcessFiles.Application.Interfaces;
using Cadena.Importacion.QueueProcessFiles.Application.Services;
using Cadena.Importacion.QueueProcessFiles.Domain.CommandHandlers;
using Cadena.Importacion.QueueProcessFiles.Domain.Commands;
using Cadena.Importacion.QueueProcessFiles.Domain.EventHandlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadena.Importacion.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ILocalConfigurationSettings, LocalConfigurationSettings>();

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory, sp.GetService<ILocalConfigurationSettings>());
            });

            services.AddTransient<IFileProcess, FileProcess>();

            //Subscriptions
            services.AddTransient<FileProcessEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<FileProcessCreateEvent>, FileProcessEventHandler>();

            ////Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateFileProcessCommand, bool>, FileProcessCommandHandlers>();

            ////Application Services
            //services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<ITransferService, TransferService>();

            ////Data
            //services.AddTransient<IAccountRepository, AccountRepository>();
            //services.AddTransient<ITransferRepository, TransferRepository>();
            //services.AddTransient<BankingDbContext>();
            //services.AddTransient<TransferDbContext>();
        }
    }
}
