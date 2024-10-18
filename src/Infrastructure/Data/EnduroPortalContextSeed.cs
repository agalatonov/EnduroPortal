using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public class EnduroPortalContextSeed
    {
        public static void SeedAsync(IConfiguration configuration, IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<EnduroPortalDBContext>();

            context.Database.EnsureDeleted();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
