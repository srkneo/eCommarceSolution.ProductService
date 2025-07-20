// File: API/Endpoints/ProductAPIEndpoints.cs

using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;

namespace eCommerce.ProductsService.API.Endpoints;

public static class ProductAPIEndpoints
{
    /// <summary>
    /// Maps product-related API endpoints.
    /// </summary>
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        // GET: /api/products
        app.MapGet("/api/products", async (IProductsService productsService) =>
        {
            var products = await productsService.GetProducts();
            return Results.Ok(products);    
        });

        // GET: /api/products/search/product-id/{ProductID}
        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (
            IProductsService productsService, Guid ProductID) =>
        {
            var product = await productsService.GetProductByCondition(p => p.ProductID == ProductID);

            return product != null ? Results.Ok(product) : Results.NotFound();
        });

        // GET: /api/products/search/{search}
        app.MapGet("/api/products/search/{search}", async (
            IProductsService productsService, string search) =>
        {
            var byName = await productsService.GetProductsByCondition(p =>
                p.ProductName != null && p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase));

            var byCategory = await productsService.GetProductsByCondition(p =>
                p.Category != null && p.Category.Contains(search, StringComparison.OrdinalIgnoreCase));

            var combined = byName.Union(byCategory).Distinct();
            return Results.Ok(combined);
        });

        // POST: /api/products
        app.MapPost("/api/products", async (
            IProductsService productsService,
            IValidator<ProductAddRequest> validator,
            ProductAddRequest request) =>
        {
            ValidationResult validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var added = await productsService.AddProduct(request);
            return added != null
                ? Results.Created($"/api/products/search/product-id/{added.ProductID}", added)
                : Results.Problem("Failed to add product");
        });

        // PUT: /api/products
        app.MapPut("/api/products", async (
            IProductsService productsService,
            IValidator<ProductUpdateRequest> validator,
            ProductUpdateRequest request) =>
        {
            ValidationResult validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var updated = await productsService.UpdateProduct(request);
            return updated != null
                ? Results.Ok(updated)
                : Results.Problem("Failed to update product");
        });

        // DELETE: /api/products/{ProductID}
        app.MapDelete("/api/products/{ProductID:guid}", async (
            IProductsService productsService, Guid ProductID) =>
        {
            bool deleted = await productsService.DeleteProduct(ProductID);
            return deleted ? Results.Ok(true) : Results.Problem("Failed to delete product");
        });

        return app; // 👈 Return the builder to allow fluent chaining
    }
}
