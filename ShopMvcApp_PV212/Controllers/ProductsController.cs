using Microsoft.AspNetCore.Mvc;
using ShopMvcApp_PV212.Models;

namespace ShopMvcApp_PV212.Controllers
{
    
    public class ProductsController : Controller
    {
        List<Product> products;
        public ProductsController()
        {
            products = new List<Product>()
            {
                new Product() { Id = 1, Name = "iPhone X", Category = "Electronics", Discount = 10, Price = 389, Quantity = 4 },
                new Product() { Id = 2, Name = "Samsung S4", Category = "Electronics", Discount = 0, Price = 440, Quantity = 0 },
                 new Product() { Id = 3, Name = "LG Smart TV", Category = "TV", Discount = 25, Price = 499, Quantity = 22 }
            };
        }
        public IActionResult Index()
        {
            // .. load data from database ..

            return View(products);
        }
    }
}
