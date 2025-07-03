// File: BusinessLogicLayer/Validators/ProductUpdateRequestValidator.cs

using FluentValidation;
using eCommerce.BusinessLogicLayer.DTO;

namespace eCommerce.BusinessLogicLayer.Validators;

/// <summary>
/// Validator for ProductUpdateRequest using FluentValidation.
/// Ensures fields for product updates are valid.
/// </summary>
public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        // ProductID must not be empty
        RuleFor(x => x.ProductID)
            .NotEmpty()
            .WithMessage("Product ID can't be blank");

        // ProductName must not be empty
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Product Name can't be blank");

        // Category must be a valid enum value
        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Category must be one of the defined options");

        // UnitPrice must be a positive number
        RuleFor(x => x.UnitPrice)
            .InclusiveBetween(0, double.MaxValue)
            .WithMessage($"Unit Price should be between 0 and {double.MaxValue}");

        // QuantityInStock must be a non-negative integer
        RuleFor(x => x.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue)
            .WithMessage($"Quantity in Stock should be between 0 and {int.MaxValue}");
    }
}
