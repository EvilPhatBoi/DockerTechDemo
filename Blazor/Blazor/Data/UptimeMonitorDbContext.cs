using Microsoft.EntityFrameworkCore;
namespace Blazor.Data
{
    public class UptimeMonitorDbContext : DbContext
    {

        public UptimeMonitorDbContext(DbContextOptions<UptimeMonitorDbContext> options) : base(options)
        {
        }

        public DbSet<Uptime> Uptime { get; set; }
    }
}