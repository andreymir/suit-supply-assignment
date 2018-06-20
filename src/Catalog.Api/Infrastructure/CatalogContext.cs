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