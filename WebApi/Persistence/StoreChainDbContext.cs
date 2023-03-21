using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Persistence
{
    public sealed class StoreChainDbContext : DbContext
    {
        public StoreChainDbContext(DbContextOptions<StoreChainDbContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Первичные ключи
            modelBuilder.Entity<Product>().HasKey(p => new { p.Id });
            modelBuilder.Entity<SalesPoint>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Buyer>().HasKey(p => new { p.Id });

            //SalesPoint - ProvidedProduct - Product
            modelBuilder.Entity<SalesPoint>()
                .HasMany(salesPoint => salesPoint.Products)
                .WithMany(product => product.SalesPoints)
                .UsingEntity<ProvidedProduct>(
                    j => j.HasOne(providedProduct => providedProduct.Product)
                        .WithMany(product => product.ProvidedProducts)
                        .HasForeignKey(providedProduct => providedProduct.ProductId)
                        .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne(providedProduct => providedProduct.SalesPoint)
                        .WithMany(salesPoint => salesPoint.ProvidedProducts)
                        .HasForeignKey(providedProduct => providedProduct.SalesPointId)
                        .OnDelete(DeleteBehavior.Cascade),

                    builder =>
                    {
                        builder.Property(providedProduct => providedProduct.ProductQuantity);
                        builder.HasKey(providedProduct => new { providedProduct.ProductId, providedProduct.SalesPointId });
                    }
                );

            //SalesPoint - SaleAsync - Buyer
            modelBuilder.Entity<SalesPoint>()
                .HasMany(salesPoint => salesPoint.Buyers)
                .WithMany(buyer => buyer.SalesPoints)
                .UsingEntity<Sale>(
                    j => j.HasOne(sale => sale.Buyer)
                        .WithMany(buyer => buyer.Sales)
                        .HasForeignKey(sale => sale.BuyerId)
                        .OnDelete(DeleteBehavior.SetNull),

                    j => j.HasOne(sale => sale.SalesPoint)
                        .WithMany(salesPoint => salesPoint.Sales)
                        .HasForeignKey(sale => sale.SalesPointId)
                        .OnDelete(DeleteBehavior.Cascade),
                    
                    builder =>
                    {
                        builder.Property(sale => sale.DateTime);
                        builder.Property(sale => sale.TotalAmount);
                        builder.HasKey(sale => sale.Id);
                    }
                );

            //SaleAsync - SalesData - Product
            modelBuilder.Entity<Sale>()
                .HasMany(sale => sale.Products)
                .WithMany(product => product.Sales)
                .UsingEntity<SalesData>(
                    j => j.HasOne(salesData => salesData.Product)
                        .WithMany(product => product.SalesDataItems)
                        .HasForeignKey(salesData => salesData.ProductId)
                        .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne(salesData => salesData.Sale)
                        .WithMany(sale => sale.SalesDataItems)
                        .HasForeignKey(salesData => salesData.SaleId)
                        .OnDelete(DeleteBehavior.Cascade),

                    builder =>
                    {
                        builder.Property(salesData => salesData.ProductQuantity);
                        builder.Property(salesData => salesData.ProductIdAmount);
                        builder.HasKey(salesData => new {salesData.ProductId, salesData.SaleId});
                    }
                );
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Buyer> Buyer { get; set; }

        public DbSet<SalesPoint> SalesPoint { get; set; }

        public DbSet<ProvidedProduct> ProvidedProduct { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<SalesData> SalesData { get; set; }
    }
}
