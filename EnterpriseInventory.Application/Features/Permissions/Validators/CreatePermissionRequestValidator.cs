using EnterpriseInventory.Application.Features.Permissions.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Features.Permissions.Validators;

/// <summary>
/// Validates requests for creating a new permission.
/// </summary>
public sealed class CreatePermissionRequestValidator
    : AbstractValidator<CreatePermissionRequest>
{
    public CreatePermissionRequestValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Permission name is required.")
            .MaximumLength(150)
                .WithMessage(
                    "Permission name cannot exceed 150 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage(
                "Permission description cannot exceed 500 characters.");
    }
}