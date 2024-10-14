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
                optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Enduro;Username=admin;Password=password");
            }
        }
    }
}
