using EnduroPortal.GrpcServer.Services;
using EnduroPortal.GrpcServer.Utils;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

Thread.Sleep(5000);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<EnduroPortalDBContext>(optional =>
    optional.UseNpgsql(Environment.GetEnvironmentVariable("PostgresDbConnection")));
builder.Services.AddScoped<IGrpcConversions, GrpcConversions>();

builder.Logging.AddConsole();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        EnduroPortalContextSeed.SeedAsync(builder.Configuration, scope);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"Error in <{nameof(Program)}>");
    }
}

app.MapGrpcService<EventsService>();
app.MapGrpcService<ParticipiantService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
