using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Services;
using System.Security.Claims;

namespace ShopMvcApp_PV212.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private string UserEmail => User.FindFirstValue(ClaimTypes.Email)!;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        public IActionResult Index()
        {
            return View(ordersService.GetOrders(UserId));
        }

        public IActionResult Create()
        {
            ordersService.Create(UserId, UserEmail);
            return RedirectToAction(nameof(Index));
        }
    }
}
