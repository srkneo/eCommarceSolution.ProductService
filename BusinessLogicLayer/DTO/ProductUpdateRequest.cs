// File: BusinessLogicLayer/DTO/ProductUpdateRequest.cs

namespace eCommerce.BusinessLogicLayer.DTO;

/// <summary>
/// DTO for updating an existing product.
/// </summary>
public record ProductUpdateRequest(
    Guid ProductID,
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    // Parameterless constructor for model binding and serialization
    public ProductUpdateRequest() : this(default, default!, default, default, default) { }
}
