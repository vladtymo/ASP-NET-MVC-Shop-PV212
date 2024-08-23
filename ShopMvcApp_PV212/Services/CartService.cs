using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Extensions;

namespace ShopMvcApp_PV212.Services
{
    public class CartService : ICartService
    {
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;
        private readonly ShopDbContext context;

        public CartService(IHttpContextAccessor contextAccessor, IMapper mapper, ShopDbContext context)
        {
            httpContext = contextAccessor.HttpContext!;
            this.mapper = mapper;
            this.context = context;
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) return 0;

            return ids.Distinct().Count();
        }

        public List<ProductDto> GetProducts() 
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items") ?? new();

            var products = context.Products.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();

            return mapper.Map<List<ProductDto>>(products);
        }

        public List<Product> GetProductsEntity()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items") ?? new();

            return context.Products.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();
        }

        public void AddItem(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) ids = new();

            ids.Add(id);

            httpContext.Session.Set("cart_items", ids);
        }

        public void RemoveItem(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null || !ids.Contains(id)) return; // throw exception

            ids.Remove(id);

            httpContext.Session.Set("cart_items", ids);
        }

        public void Clear()
        {
            httpContext.Session.Remove("cart_items");
        }
    }
}
