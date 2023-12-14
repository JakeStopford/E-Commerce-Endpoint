namespace Data.Entities;

public class CustomerEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public List<OrderEntity> Orders { get; set; }
}