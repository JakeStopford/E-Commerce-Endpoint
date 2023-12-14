namespace Data.Entities;

public class ShippingAddressEntity
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostCode { get; set; }
}