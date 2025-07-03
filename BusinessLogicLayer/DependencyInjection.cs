// BusinessLogicLayer/DependencyInjection.cs
using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductsService.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //services.AddScoped<IProductsService, ProductsService>();
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        return services;
    }
}
