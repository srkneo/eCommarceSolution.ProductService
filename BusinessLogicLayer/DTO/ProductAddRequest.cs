namespace eCommerce.BusinessLogicLayer.DTO;

/// <summary>
/// DTO for adding a new product.
/// </summary>
public record ProductAddRequest(
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    // Parameterless constructor required for model binding or serialization
    public ProductAddRequest() : this(default!, default, default, default) { }
}
