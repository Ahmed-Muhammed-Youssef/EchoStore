using Microsoft.EntityFrameworkCore;
using Core.Entities;
namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Deleting a brand or a product type cause the profram to delete
            // All products of this brand/ type
            // This is done to pervent products with null brand/type
            modelBuilder.Entity<ProductInfo>()
                .HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.ProductBrandId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductInfo>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductInfo)
                .WithMany()
                .HasForeignKey(p => p.ProductInfoId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}