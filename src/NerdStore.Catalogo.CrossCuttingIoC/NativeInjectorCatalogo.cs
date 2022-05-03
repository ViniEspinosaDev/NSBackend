using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain.Interface;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.Bus;

namespace NerdStore.Catalogo.CrossCuttingIoC
{
    public class NativeInjectorCatalogo
    {
        public static void RegistrarDependencias(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            RegistrarServices(services);
            RegistrarRepositories(services);

            services.AddScoped<CatalogoContext>();
        }

        private static void RegistrarEvents(IServiceCollection services)
        {
            //services.AddScoped<INotificationHandler>
        }

        private static void RegistrarServices(IServiceCollection services)
        {
            services.AddScoped<IEstoqueService, EstoqueService>();
        }

        private static void RegistrarRepositories(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
