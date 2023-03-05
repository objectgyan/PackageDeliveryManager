using DeliveryManager.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace DeliveryManager.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Recipient> Recipients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "deliverymanagerdb");
            }
        }
    }
}