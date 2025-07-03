// File: BusinessLogicLayer/Services/ProductsService.cs

using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services;

/// <summary>
/// Business logic for managing products.
/// </summary>
public class ProductsService : IProductsService
{
    private readonly IValidator<ProductAddRequest> _addValidator;
    private readonly IValidator<ProductUpdateRequest> _updateValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _repository;

    public ProductsService(
        IValidator<ProductAddRequest> addValidator,
        IValidator<ProductUpdateRequest> updateValidator,
        IMapper mapper,
        IProductsRepository repository)
    {
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        // Validate the request using FluentValidation
        ValidationResult validation = await _addValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            string errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Product product = _mapper.Map<Product>(request);
        Product? addedProduct = await _repository.AddProduct(product);

        return addedProduct is null ? null : _mapper.Map<ProductResponse>(addedProduct);
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        Product? existingProduct = await _repository.GetProductByCondition(p => p.ProductID == request.ProductID);
        if (existingProduct == null)
            throw new ArgumentException("Product not found");

        ValidationResult validation = await _updateValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            string errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Product product = _mapper.Map<Product>(request);
        Product? updatedProduct = await _repository.UpdateProduct(product);

        return updatedProduct is null ? null : _mapper.Map<ProductResponse>(updatedProduct);
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product? existingProduct = await _repository.GetProductByCondition(p => p.ProductID == productID);
        if (existingProduct == null)
            return false;

        return await _repository.DeleteProduct(productID);
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> products = await _repository.GetProducts();

        return _mapper.Map<IEnumerable<ProductResponse?>>(products).ToList();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
    {
        IEnumerable<Product?> products = await _repository.GetProductsByCondition(condition);

        return _mapper.Map<IEnumerable<ProductResponse?>>(products).ToList();
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> condition)
    {
        Product? product = await _repository.GetProductByCondition(condition);

        if (product is null)
            return null;

        return product is null ? null : _mapper.Map<ProductResponse>(product);
    }
}
