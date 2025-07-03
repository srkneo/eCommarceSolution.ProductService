namespace eCommerce.BusinessLogicLayer.DTO;

/// <summary>
/// DTO used for sending product data in responses.
/// </summary>
public record ProductResponse(
    Guid ProductID,
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    // Parameterless constructor for model binding and serialization
    public ProductResponse() : this(default, default!, default, default, default) { }
}
