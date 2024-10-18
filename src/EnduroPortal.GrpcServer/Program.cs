using EnduroPortal.GrpcServer.Services;
using Infrastructure;
using Infrastructure.Data;

Thread.Sleep(5000);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<EnduroPortalDBContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

    try
    {
        EnduroPortalContextSeed.SeedAsync(builder.Configuration, scope);
    }
    catch (Exception)
    {
        //logger.LogError(ex, $"Error in <{nameof(Program)}>");
    }
}

// Configure the HTTP request pipeline.
app.MapGrpcService<EventsService>();
app.MapGrpcService<ParticipiantService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
