using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repository;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interface;
using NerdStore.Catalogo.Domain.Services;

namespace NerdStore.Catalogo.CrossCuttingIoC
{
    public class NativeInjectorCatalogo
    {
        public static void RegistrarDependencias(IServiceCollection services, IConfiguration config)
        {
            RegistrarServices(services);
            RegistrarRepositories(services);
            RegistrarEvents(services);

            services.AddScoped<CatalogoContext>();
        }

        private static void RegistrarEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
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
