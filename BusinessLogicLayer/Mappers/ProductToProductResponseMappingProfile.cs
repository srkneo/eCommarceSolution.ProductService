// File: BusinessLogicLayer/Mappers/ProductToProductResponseMappingProfile.cs

using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;

namespace eCommerce.BusinessLogicLayer.Mappers;

/// <summary>
/// AutoMapper profile to map Product entity to ProductResponse DTO.
/// </summary>
public class ProductToProductResponseMappingProfile : Profile
{
    public ProductToProductResponseMappingProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<CategoryOptions>(src.Category)))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
