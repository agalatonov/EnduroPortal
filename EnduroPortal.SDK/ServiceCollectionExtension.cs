using EnduroPortal.GrpcServer;
using Microsoft.Extensions.DependencyInjection;

namespace EnduroPortal.SDK
{
    public static class ServiceCollectionExtension
    {
        public static void AddGrpcSDK(this IServiceCollection services)
        {
            services.AddGrpcClient<Events.EventsClient>(client =>
            {
                client.Address = new Uri("https://localhost:7265");
            });

            services.AddScoped<IEventsActionGrpcService, EventsActionGrpcService>();
        }
    }
}
