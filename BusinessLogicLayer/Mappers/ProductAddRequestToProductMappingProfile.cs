// File: BusinessLogicLayer/Mappers/ProductAddRequestToProductMappingProfile.cs

using AutoMapper;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.BusinessLogicLayer.DTO;

namespace eCommerce.BusinessLogicLayer.Mappers;

/// <summary>
/// AutoMapper profile to map ProductAddRequest to Product entity.
/// </summary>
public class ProductAddRequestToProductMappingProfile : Profile
{
    public ProductAddRequestToProductMappingProfile()
    {
        CreateMap<ProductAddRequest, Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.ProductID, opt => opt.Ignore()); // ProductID is generated in DB
    }
}
