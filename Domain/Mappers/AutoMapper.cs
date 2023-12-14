using AutoMapper;
using Data.Entities;
using Domain.Models;

namespace Domain.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<OrderToSaveDto, OrderDto>();
            CreateMap<Address, ShippingAddressEntity>();
            CreateMap<Product, ProductEntity>();
        }
    }
}
