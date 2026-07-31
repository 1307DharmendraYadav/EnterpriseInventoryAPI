using EnterpriseInventory.Application.Features.RolePermissions.DTOs;
using FluentValidation;

namespace EnterpriseInventory.Application.Features.RolePermissions.Validators;

/// <summary>
/// Validates a role-permission assignment request.
/// </summary>
public class AssignPermissionsRequestValidator
    : AbstractValidator<AssignPermissionsRequest>
{
    public AssignPermissionsRequestValidator()
    {
        RuleFor(request => request.PermissionIds)
            .NotNull()
            .WithMessage("PermissionIds collection is required.");

        RuleForEach(request => request.PermissionIds)
            .GreaterThan(0)
            .WithMessage("Permission Id must be greater than zero.");
    }
}