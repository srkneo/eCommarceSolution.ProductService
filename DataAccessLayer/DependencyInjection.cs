// DataAccessLayer/DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore;
//using eCommerce.DataAccessLayer.Context;
//using eCommerce.DataAccessLayer.Repositories;
//using eCommerce.DataAccessLayer.RepositoryContracts;

namespace eCommerce.ProductsService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));

        //services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}
