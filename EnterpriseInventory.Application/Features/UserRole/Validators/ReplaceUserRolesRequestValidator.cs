using EnterpriseInventory.Application.Features.UserRole.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Validators;

public sealed class ReplaceUserRolesRequestValidator
    : AbstractValidator<ReplaceUserRolesRequest>
{
    public ReplaceUserRolesRequestValidator()
    {
        RuleFor(x => x.RoleIds)
            .NotNull()
            .WithMessage("RoleIds are required.");

        RuleFor(x => x.RoleIds)
            .Must(x => x.Count > 0)
            .WithMessage("At least one role must be assigned.");

        RuleFor(x => x.RoleIds)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("Duplicate roles are not allowed.");
    }
}