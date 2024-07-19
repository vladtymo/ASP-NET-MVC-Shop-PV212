using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Core.Dtos;
using Data.Entities;

namespace ShopMvcApp_PV212.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ShopDbContext ctx;

        public ProductsController(IMapper mapper, ShopDbContext context)
        {
            this.mapper = mapper;
            this.ctx = context;
        }

        // [C]reate [R]ead [U]pdate [D]elete

        public IActionResult Index()
        {
            // .. load data from database ..
            var products = ctx.Products
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => !x.Archived)
                .ToList();

            return View(mapper.Map<List<ProductDto>>(products));
        }

        public IActionResult Archive()
        {
            // .. load data from database ..
            var products = ctx.Products
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => x.Archived)
                .ToList();

            return View(mapper.Map<List<ProductDto>>(products));
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
        public IActionResult Create(ProductDto model)
        {
            // data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                LoadCategories();
                return View("Upsert", model);
            }

            // 1 - manual mapping
            //var entity = new Product
            //{
            //    Name = model.Name,
            //    Archived = model.Archived,
            //    CategoryId = model.CategoryId,
            //    Description = model.Description,
            //    Discount = model.Discount,
            //    ImageUrl = model.ImageUrl,
            //    Price = model.Price,
            //    Quantity = model.Quantity
            //};
            // 2 - using Auto Mapper
            var entity = mapper.Map<Product>(model);

            ctx.Products.Add(entity);
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
            return View("Upsert", mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public IActionResult Edit(ProductDto model)
        {
            // data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = false;
                LoadCategories();
                return View("Upsert", model);
            }

            ctx.Products.Update(mapper.Map<Product>(model));
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
