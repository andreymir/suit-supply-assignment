using Catalog.Api.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catalog.Api.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .Property(p => p.Code)
                .IsRequired();
            
            builder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();
            
            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            
            // Configure to auto update LastUpdated value
            builder.Entity<Product>()
                .Property(p => p.LastUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETUTCDATE()")
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            // Configure LastUpdated column as concurrency token
            builder.Entity<Product>()
                .Property(p => p.LastUpdated)
                .IsConcurrencyToken();
        }
    }
}