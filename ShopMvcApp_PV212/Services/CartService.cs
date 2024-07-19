using AutoMapper;
using Core.Dtos;
using ShopMvcApp_PV212.Extensions;

namespace ShopMvcApp_PV212.Services
{
    public class CartService
    {
        private readonly HttpContext httpContext;
        public CartService(IHttpContextAccessor contextAccessor)
        {
            httpContext = contextAccessor.HttpContext!;
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            if (ids == null) return 0;

            return ids.Distinct().Count();
        }
    }
}
