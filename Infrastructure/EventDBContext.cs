using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class EventDBContext : DbContext
    {
        public DbSet<Event> Events;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Enduro;Username=admin;Password=password");
            }
        }
    }
}
