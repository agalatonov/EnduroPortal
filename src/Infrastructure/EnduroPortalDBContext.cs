using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class EnduroPortalDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participiant> Participiants { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=app_db;Port=5433;Database=EnduroPortal;Username=admin;Password=password");
            }
        }
    }
}
