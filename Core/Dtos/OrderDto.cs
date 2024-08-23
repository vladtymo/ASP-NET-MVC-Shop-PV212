namespace Core.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public int ProductsCount { get; set; }
    }
}
