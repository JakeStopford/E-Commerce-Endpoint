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
            if (string.IsNullOrEmpty(order.CustomerEmail) || order.ShippingAddress == null || !order.Products.Any())
            {
                throw new ArgumentException("Invalid order data. Customer email, shipping address, and products are required.");
            }

            var productEntities = order.Products.Select(product => _mapper.Map<ProductEntity>(product)).ToList();

            if (!await _repository.CheckIfProductsExist(productEntities))
            {
                throw new ArgumentException("Invalid product information.");
            }

            try
            {
                var customer = await _repository.GetCustomerByEmailAsync(order.CustomerEmail);
                if (customer == null)
                {
                    customer = MapCustomer(order.CustomerEmail);
                    await _repository.AddCustomerAsync(customer);
                }

                var shippingAddress = await _repository.GetShippingAddressAsync(_mapper.Map<ShippingAddressEntity>(order.ShippingAddress));
                if (shippingAddress == null)
                {
                    shippingAddress = MapShippingAddress(order.ShippingAddress);
                    await _repository.AddShippingAddressAsync(shippingAddress);
                }

                var orderEntity = new OrderEntity
                {
                    Customer = customer,
                    OrderDate = DateTime.Now,
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

        private static CustomerEntity MapCustomer(string email)
        {
            return new CustomerEntity
            {
                Email = email
            };
        }

        private static ShippingAddressEntity MapShippingAddress(Address address)
        {
            return new ShippingAddressEntity
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                Country = address.Country,
                PostCode = address.PostCode
            };
        }
    }
}
