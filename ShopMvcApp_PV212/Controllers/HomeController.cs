using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Models;
using System.Diagnostics;

namespace ShopMvcApp_PV212.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopDbContext context;

        public HomeController(ShopDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var products = context.Products
                .Include(x => x.Category)
                .ToList();
            return View(products); // search for view in: ~/Views/Home/Index
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
