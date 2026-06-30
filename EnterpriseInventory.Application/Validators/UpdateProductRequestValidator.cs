using EnterpriseInventory.Application.DTOs;
using FluentValidation;
namespace EnterpriseInventory.Application.Validators
{


    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                 .WithMessage("Product name is required.")
                .MaximumLength(100)
                 .WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");
        }
        /* Note:

            CreateProductRequestValidator cannot be included here because it validates
            CreateProductRequest, whereas this validator validates UpdateProductRequest.

            Include() only works when both validators are for the SAME model type.

            If Create and Update DTOs have identical properties and validation rules,
            the recommended enterprise approach is to create a common generic base
            validator instead of duplicating rules.

            Example:

            public abstract class ProductRequestValidator<T> : AbstractValidator<T>
                where T : IProductRequest
            {
                // Common validation rules
            }

            public class CreateProductRequestValidator
                : ProductRequestValidator<CreateProductRequest> { }

            public class UpdateProductRequestValidator
                : ProductRequestValidator<UpdateProductRequest> { }

            Here, 'where T : IProductRequest' is a Generic Constraint.
            It ensures that T implements IProductRequest, allowing the base validator
            to access common properties (Name, Price, Quantity) for validation.

        */

    }
}
