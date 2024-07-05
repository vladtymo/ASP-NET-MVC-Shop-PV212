using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // [C]reate [R]ead [U]pdate [D]elete

        public IActionResult Index()
        {
            // .. load data from database ..
            var products = ctx.Products
                .Include(x => x.Category) // LEFT JOIN  
                .ToList();

            return View(products);
        }

        public IActionResult Delete(int id)
        {
            var product = ctx.Products.Find(id);

            if (product == null) return NotFound();

            ctx.Products.Remove(product);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
