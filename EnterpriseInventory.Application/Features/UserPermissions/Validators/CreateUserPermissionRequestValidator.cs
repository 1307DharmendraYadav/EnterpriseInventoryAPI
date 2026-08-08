using EnterpriseInventory.Application.Features.UserPermissions.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Features.UserPermissions.Validators;

public sealed class CreateUserPermissionRequestValidator
    : AbstractValidator<CreateUserPermissionRequest>
{
    public CreateUserPermissionRequestValidator()
    {
        RuleFor(x => x.PermissionId)
            .GreaterThan(0)
            .WithMessage("PermissionId must be greater than zero.");

        RuleFor(x => x.IsAllowed)
            .NotNull()
            .WithMessage("IsAllowed is required.");
    }
}