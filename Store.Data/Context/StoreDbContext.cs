using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
namespace Store.Data.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  -->> Use Less
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }


    }
}
