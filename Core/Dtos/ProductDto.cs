using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool Archived { get; set; }
    }
}
