using Data.Entities;
using Microsoft.EntityFrameworkCore.Storage;

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
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task RollbackTransactionAsync(IDbContextTransaction transaction);
}