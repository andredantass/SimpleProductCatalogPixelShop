
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SimpleProductCatalog.Domain.Entities;

namespace SimpleProductCatalog.Infra.Data.DBContext
{
    public class SimpleProductCatalogDBContext : DbContext
    {
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ProductCatalogDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            

            modelBuilder.Ignore<Notification>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
