using AutoMapper;
using Data.Entities;
using Data.Repositories.Interfaces;
using Domain.Models;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> SaveOrder(OrderToSaveDto order)
        {
            // Validation - Check if required fields are provided
            if (string.IsNullOrEmpty(order.CustomerEmail) || order.ShippingAddress == null || !order.Products.Any())
            {
                throw new ArgumentException("Invalid order data. Customer email, shipping address, and products are required.");
            }

            // Validation - Other custom validation logic if needed

            try
            {
                var customer = await _repository.GetCustomerByEmailAsync(order.CustomerEmail);
                if (customer == null)
                {
                    customer = new CustomerEntity
                    {
                        Email = order.CustomerEmail
                    };
                    await _repository.AddCustomerAsync(customer);
                }

                var shippingAddress = await _repository.GetShippingAddressAsync(_mapper.Map<ShippingAddressEntity>(order.ShippingAddress));
                if (shippingAddress == null)
                {
                    shippingAddress = new ShippingAddressEntity
                    {
                        AddressLine1 = order.ShippingAddress.AddressLine1,
                        AddressLine2 = order.ShippingAddress.AddressLine2,
                        City = order.ShippingAddress.City,
                        Country = order.ShippingAddress.Country,
                        PostCode = order.ShippingAddress.PostCode
                    };
                    await _repository.AddShippingAddressAsync(shippingAddress);
                }

                var orderEntity = new OrderEntity
                {
                    Customer = customer,
                    OrderDate = order.OrderDate,
                    ShippingAddress = shippingAddress
                };

                var productOrderEntities = order.Products.Select(product =>
                    new ProductOrderEntity
                    {
                        OrderId = orderEntity.Id,
                        ProductId = product.ProductId,
                        ProductQuantity = product.Quantity
                    }).ToList();

                orderEntity.ProductOrders = productOrderEntities;

                await _repository.AddOrderAsync(orderEntity);
                await _repository.SaveChangesAsync();

                return orderEntity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving order: {ex.Message}");
                throw;
            }
        }
    }
}
