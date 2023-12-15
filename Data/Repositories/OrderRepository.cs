using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CustomerEntity?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<ShippingAddressEntity?> GetShippingAddressAsync(ShippingAddressEntity address)
        {
            return await _context.ShippingAddresses.FirstOrDefaultAsync(x =>
                x.AddressLine1 == address.AddressLine1 &&
                x.AddressLine2 == address.AddressLine2 &&
                x.City == address.City &&
                x.Country == address.Country &&
                x.PostCode == address.PostCode);
        }

        public async Task<int> AddCustomerAsync(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddShippingAddressAsync(ShippingAddressEntity shippingAddress)
        {
            _context.ShippingAddresses.Add(shippingAddress);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddOrderAsync(OrderEntity order)
        {
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfProductsExist(List<ProductEntity> products)
        {
            var productIds = products.Select(p => p.Id);
            return await _context.Products.AnyAsync(x => productIds.Contains(x.Id));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
