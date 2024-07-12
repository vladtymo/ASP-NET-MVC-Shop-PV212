using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            LoadCategories();
            ViewBag.CreateMode = true;
            return View("Upsert");
        }

        // POST
        [HttpPost]
        public IActionResult Create(Product model)
        {
            // data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                LoadCategories();
                return View("Upsert", model);
            }

            ctx.Products.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = ctx.Products.Find(id);

            if (product == null) return NotFound();

            LoadCategories();
            ViewBag.CreateMode = false;
            return View("Upsert", product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            // data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = false;
                LoadCategories();
                return View("Upsert", model);
            }

            ctx.Products.Update(model);
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

        private void LoadCategories()
        {
            // ViewBag.PropertyName = value;
            ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
        }
    }
}
