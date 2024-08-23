using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ShopDbContext context;
        private readonly IMapper mapper;
        private readonly ICartService cartService;
        private readonly IEmailSender emailSender;

        public OrdersService(ShopDbContext context, IMapper mapper, ICartService cartService, IEmailSender emailSender)
        {
            this.context = context;
            this.mapper = mapper;
            this.cartService = cartService;
            this.emailSender = emailSender;
        }
        public async Task Create(string userId, string userEmail)
        {
            // create order
            var newOrder = new Order()
            {
                CreatedAt = DateTime.Now,
                Products = cartService.GetProductsEntity(),
                UserId = userId
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();

            // email client about new order
            await emailSender.SendEmailAsync(userEmail, $"New Order #{newOrder.Id}", "<h1>New Order</h1>");

            cartService.Clear();
        }

        public List<OrderDto> GetOrders(string userId)
        {
            var orders = context.Orders.Include(x => x.User)
                                        .Where(x => x.UserId == userId)
                                        .ToList();
            return mapper.Map<List<OrderDto>>(orders);
        }
    }
}
