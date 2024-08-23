using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Extensions;
using ShopMvcApp_PV212.Services;

namespace ShopMvcApp_PV212.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly RoleManager<IdentityRole> userManager;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(cartService.GetProducts());
        }

        public IActionResult Add(int id, string? returnUrl)
        {
            cartService.AddItem(id);
            return Redirect(returnUrl ?? "/");
        }

        public IActionResult Remove(int id)
        {
            cartService.RemoveItem(id);
            return RedirectToAction("Index");
        }
    }
}
