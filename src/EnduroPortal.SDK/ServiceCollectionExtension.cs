using EnduroPortal.GrpcServer;
using EnduroPortal.SDK.GrpcServices;
using EnduroPortal.SDK.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EnduroPortal.SDK
{
    public static class ServiceCollectionExtension
    {
        public static void AddGrpcSDK(this IServiceCollection services)
        {
            services.AddGrpcClient<Events.EventsClient>(client =>
            {
                client.Address = new Uri("http://grpc_server:7265");
            });
            services.AddGrpcClient<Participiant.ParticipiantClient>(client =>
            {
                client.Address = new Uri("http://grpc_server:7265");
            });

            services.AddScoped<IEventsActionGrpcService, EventsActionGrpcService>();
            services.AddScoped<IParticipiantGrpcService, ParticipiantGrpcService>();

        }
    }
}
