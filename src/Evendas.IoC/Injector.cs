using Evendas.Application.Interfaces;
using Evendas.Application.ServiceBus;
using Evendas.Application.Services;
using Evendas.Data.Context;
using Evendas.Data.Repositories;
using Evendas.Data.UnitOfWork;
using Evendas.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Evendas.IoC
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // App
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            // Data
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EvendasContext>();

            // ServiceBus
            services.AddScoped<IServiceBusSender, ServiceBusSender>();
            services.AddScoped<IServiceBusTopicSubscription, ServiceBusTopicSubscription>();
        }
    }
}
