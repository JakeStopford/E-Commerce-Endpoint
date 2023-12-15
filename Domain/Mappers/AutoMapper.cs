using AutoMapper;
using Data.Entities;
using Domain.Models;

namespace Domain.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Address, ShippingAddressEntity>();
            CreateMap<Product, ProductEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
