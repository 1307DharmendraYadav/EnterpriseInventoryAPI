using EnterpriseInventory.Application.Features.Roles.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Validators;

/// <summary>
/// Validates requests for updating an existing role.
/// </summary>
public sealed class UpdateRoleRequestValidator
    : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Role name is required.")
            .MaximumLength(100)
                .WithMessage("Role name cannot exceed 100 characters.");
    }
}