using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        //[Required]
        public string Name { get; set; }
        // [Url]
        public string? ImageUrl { get; set; }
        //[MaxLength(100)]
        public string? Description { get; set; }

        //[Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        // [Range(0, 100)]
        public int Discount { get; set; }
        // [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public bool Archived { get; set; }

        // ---- navigation properties
        public Category? Category { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
