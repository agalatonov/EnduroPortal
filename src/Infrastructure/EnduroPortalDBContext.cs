using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class EnduroPortalDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participiant> Participiants { get; set; }

        public EnduroPortalDBContext(DbContextOptions<EnduroPortalDBContext> options) : base(options)
        {

        }
    }
}
