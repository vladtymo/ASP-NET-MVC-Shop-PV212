namespace Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        // ---- navigation properties
        public ICollection<Product>? Products { get; set; }
        public User User { get; set; }
    }
}
