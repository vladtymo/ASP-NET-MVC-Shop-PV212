using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Data;
using ShopMvcApp_PV212.Entities;
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
                .Where(x => !x.Archived)
                .ToList();

            return View(products);
        }

        public IActionResult Archive()
        {
            // .. load data from database ..
            var products = ctx.Products
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => x.Archived)
                .ToList();

            return View(products);
        }

        // GET: 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Product model)
        {
            // TODO: add data validation

            ctx.Products.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = ctx.Products.Find(id);

            if (product == null) return NotFound();

            ctx.Products.Remove(product);
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }

        public IActionResult ArchiveItem(int id)
        {
            var product = ctx.Products.Find(id);

            if (product == null) return NotFound();

            product.Archived = true;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RestoreItem(int id)
        {
            var product = ctx.Products.Find(id);

            if (product == null) return NotFound();

            product.Archived = false;
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }
    }
}
