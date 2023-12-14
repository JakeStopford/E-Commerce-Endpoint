namespace Data.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductOrderEntity> ProductOrders { get; set; }
    public int ShippingAddressId { get; set; }
    public ShippingAddressEntity ShippingAddress { get; set; }
}