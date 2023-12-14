namespace Domain.Models
{
    public class OrderDto
    {
        public string CustomerEmail { get; }
        public IEnumerable<Product> Products { get; }
        public DateTime OrderDate { get; }
    }
}
