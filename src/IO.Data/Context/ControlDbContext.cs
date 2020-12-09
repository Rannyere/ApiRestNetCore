using System.Linq;
using IO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace IO.Data.Context
{
    public class ControlDbContext : DbContext
    {
        public ControlDbContext(DbContextOptions options) : base (options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // column size guarantee
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            // register all entities linked to DbContext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ControlDbContext).Assembly);

            // configuration to prevent cascading delete in DB
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
