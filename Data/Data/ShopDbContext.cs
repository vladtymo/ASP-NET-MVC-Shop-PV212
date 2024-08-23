using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.Data
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShopDbContext() { }
        public ShopDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopMvc_PV212;Integrated Security=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new[]
            {
                new Category() { Id = 1, Name = "Electronics" },
                new Category() { Id = 2, Name = "Sport" },
                new Category() { Id = 3, Name = "Fashion" },
                new Category() { Id = 4, Name = "Home & Garden" },
                new Category() { Id = 5, Name = "Transport" },
                new Category() { Id = 6, Name = "Toys & Hobbies" },
                new Category() { Id = 7, Name = "Musical Instruments" },
                new Category() { Id = 8, Name = "Art" },
                new Category() { Id = 9, Name = "Other" }
            });

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product() { Id = 1, Name = "iPhone X", CategoryId = 1, Discount = 10, Price = 650, Quantity = 5, ImageUrl = "https://applecity.com.ua/image/cache/catalog/0iphone/ipohnex/iphone-x-black-1000x1000.png"  },

                new Product() { Id = 2, Name = "PowerBall", CategoryId = 2, Price = 45.5M, Quantity = 3, ImageUrl = "https://http2.mlstatic.com/D_NQ_NP_727192-CBT53879999753_022023-V.jpg" },

                new Product() { Id = 3, Name = "Nike T-Shirt", CategoryId = 3, Discount = 15, Price = 189, Quantity = 3, ImageUrl = "https://www.seekpng.com/png/detail/316-3168852_nike-air-logo-t-shirt-nike-t-shirt.png"  },

                new Product() { Id = 4, Name = "Samsung S23", CategoryId = 1, Discount = 5, Price = 1200, Quantity = 0, ImageUrl = "https://sota.kh.ua/image/cache/data/Samsung-2/samsung-s23-s23plus-blk-01-700x700.webp" },

                new Product() { Id = 5, Name = "Air Ball", CategoryId = 6, Price = 50, Quantity = 0,  ImageUrl = "https://cdn.shopify.com/s/files/1/0046/1163/7320/products/69ee701e-e806-4c4d-b804-d53dc1f0e11a_grande.jpg" },

                new Product() { Id = 6, Name = "MacBook Pro 2019", CategoryId = 1, Discount = 10, Quantity = 23, ImageUrl = "https://newtime.ua/image/import/catalog/mac/macbook_pro/MacBook-Pro-16-2019/MacBook-Pro-16-Space-Gray-2019/MacBook-Pro-16-Space-Gray-00.webp" },

                new Product() { Id = 7, Name = "Samsung S4", CategoryId = 2, Discount = 0, Price = 440, Quantity = 0 },
            });
        }
    }
}
