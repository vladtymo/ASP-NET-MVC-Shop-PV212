using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Extensions;

namespace ShopMvcApp_PV212.Controllers
{
    public class CartController : Controller
    {
        private ShopDbContext context = new();
        private readonly IMapper mapper;

        public CartController(IMapper mapper)
        {
            this.mapper = mapper;
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
