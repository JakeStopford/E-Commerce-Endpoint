using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class OrderToSaveDto
{
    [Required(ErrorMessage = "Customer email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string CustomerEmail { get; set; }
    public Address ShippingAddress { get; set;  }
    public IEnumerable<Product> Products { get; set; }
    public DateTime OrderDate { get; set; }
}