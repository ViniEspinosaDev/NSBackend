using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.CrossCuttingIoc
{
    public class NativeInjectorCore
    {
        public static void RegistrarDependencias(IServiceCollection services, IConfiguration config)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
