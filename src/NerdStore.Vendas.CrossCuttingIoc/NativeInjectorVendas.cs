using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Data;
using NerdStore.Vendas.Data.Repository;
using NerdStore.Vendas.Domain.Interface;

namespace NerdStore.Vendas.CrossCuttingIoc
{
    public class NativeInjectorVendas
    {
        public static void RegistrarDependencias(IServiceCollection services, IConfiguration config)
        {
            //RegistrarServices(services);
            RegistrarRepositories(services);
            RegistrarCommands(services);
            RegistrarEvents(services);

            services.AddScoped<VendasContext>();
        }

        private static void RegistrarEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemRemovidoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<VoucherAplicadoPedidoEvent>, PedidoEventHandler>();
        }

        private static void RegistrarRepositories(IServiceCollection services)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
        }

        private static void RegistrarCommands(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
        }
    }
}
