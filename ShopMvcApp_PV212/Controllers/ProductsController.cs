using Microsoft.AspNetCore.Mvc;
using ShopMvcApp_PV212.Data;
using ShopMvcApp_PV212.Models;

namespace ShopMvcApp_PV212.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext ctx = new ShopDbContext();
        public ProductsController()
        { 
        }

        public IActionResult Index()
        {
            // .. load data from database ..
            var products = ctx.Products.ToList();

            return View(products);
        }
    }
}
