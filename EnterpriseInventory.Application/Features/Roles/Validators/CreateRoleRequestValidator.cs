using EnterpriseInventory.Application.Features.Roles.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Features.Roles.Validators;

/// <summary>
/// Validates requests for creating a new role.
/// </summary>
public sealed class CreateRoleRequestValidator
    : AbstractValidator<CreateRoleRequest>
{
    /*
        Without Cascade(CascadeMode.Stop), FluentValidation checks:
        NotEmpty()
        MaximumLength()

        Although you'll usually only see one error here, for longer rule chains
        you may end up evaluating unnecessary rules.

        With Cascade(CascadeMode.Stop):

        NotEmpty()
        Stops immediately
        Doesn't evaluate MaximumLength()

        This is a common pattern in production code because it avoids unnecessary
        validations once a prerequisite rule has already failed.
    */

    public CreateRoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Role name is required.")
            .MaximumLength(100)
                .WithMessage("Role name cannot exceed 100 characters.");
    }
}