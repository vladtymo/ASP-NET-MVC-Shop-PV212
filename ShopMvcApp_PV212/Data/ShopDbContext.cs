using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Models;

namespace ShopMvcApp_PV212.Data
{
    public class ShopDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopMvc_PV212;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product() { Id = 1, Name = "iPhone X", Category = "Electronics", Discount = 10, Price = 389, Quantity = 4 },
                new Product() { Id = 2, Name = "Samsung S4", Category = "Electronics", Discount = 0, Price = 440, Quantity = 0 },
                 new Product() { Id = 3, Name = "LG Smart TV", Category = "TV", Discount = 25, Price = 499, Quantity = 22 }
            });
        }

        public DbSet<Product> Products { get; set; }
    }
}
