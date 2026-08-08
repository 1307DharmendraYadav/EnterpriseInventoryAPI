using EnterpriseInventory.Application.Features.UserPermissions.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Features.UserPermissions.Validators;

public sealed class UpdateUserPermissionRequestValidator
    : AbstractValidator<UpdateUserPermissionRequest>
{
    public UpdateUserPermissionRequestValidator()
    {
        RuleFor(x => x.IsAllowed)
            .NotNull()
            .WithMessage("IsAllowed is required.");
    }
}