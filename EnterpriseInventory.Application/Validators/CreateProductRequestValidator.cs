using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseInventory.Application.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        /*
            Without Cascade(CascadeMode.Stop), FluentValidation checks:
            ❌ NotEmpty()
            ❌ MaximumLength()
            
            Although you'll usually only see one error here, for longer rule chains you     may     end     up evaluating unnecessary rules.
            
            With Cascade(CascadeMode.Stop):
            
            ❌ NotEmpty()
            Stops immediately
            Doesn't evaluate MaximumLength()
            
            This is a common pattern in production code because it avoids unnecessary       validations   once a prerequisite rule has already failed.
         */
        RuleFor(x => x.Name)
          .Cascade(CascadeMode.Stop)
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
}
