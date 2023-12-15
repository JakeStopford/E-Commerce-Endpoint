using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<CustomerEntity?> GetCustomerByEmailAsync(string email);
    Task<ShippingAddressEntity?> GetShippingAddressAsync(ShippingAddressEntity address);
    Task<int> AddCustomerAsync(CustomerEntity customer);
    Task<int> AddShippingAddressAsync(ShippingAddressEntity? shippingAddress);
    Task<int> AddOrderAsync(OrderEntity order);
    Task<bool> CheckIfProductsExist(List<ProductEntity> products);
    Task<int> SaveChangesAsync();
}