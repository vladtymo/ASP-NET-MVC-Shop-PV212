using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Extensions;
using ShopMvcApp_PV212.Services;

namespace ShopMvcApp_PV212.Controllers
{
    public class CartController : Controller
    {
        private readonly IMapper mapper;
        private readonly ShopDbContext context;

        public CartController(IMapper mapper, ShopDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items") ?? new();

            var products = context.Products.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();

            return View(mapper.Map<List<ProductDto>>(products));
        }

        public IActionResult Add(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) ids = new();

            ids.Add(id);

            HttpContext.Session.Set("cart_items", ids);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items");

            if (ids == null || !ids.Contains(id)) return NotFound();

            ids.Remove(id);

            HttpContext.Session.Set("cart_items", ids);

            return RedirectToAction("Index");
        }
    }
}
