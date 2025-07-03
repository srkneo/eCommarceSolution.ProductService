// File: BusinessLogicLayer/ServiceContracts/IProductsService.cs

using DataAccessLayer.Entities;
using eCommerce.BusinessLogicLayer.DTO;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts;

/// <summary>
/// Interface for managing product-related operations.
/// </summary>
public interface IProductsService
{
    /// <summary>
    /// Retrieves the list of all products.
    /// </summary>
    Task<List<ProductResponse?>> GetProducts();

    /// <summary>
    /// Retrieves products that match the given condition.
    /// </summary>
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Retrieves a single product that matches the given condition.
    /// </summary>
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// Adds a new product to the repository.
    /// </summary>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    Task<bool> DeleteProduct(Guid productID);
}
