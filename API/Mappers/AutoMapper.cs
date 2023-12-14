using AutoMapper;
using Data.Entities;
using Domain.Models;

namespace API.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<OrderToSaveDto, OrderDto>();
            CreateMap<Address, ShippingAddressEntity>();
        }
    }
}
