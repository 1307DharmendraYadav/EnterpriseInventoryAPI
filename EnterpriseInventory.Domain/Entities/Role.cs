using EnterpriseInventory.Domain.Common;

namespace EnterpriseInventory.Domain.Entities;

public class Role:BaseEntity
{

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Navigation property
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    // Navigation Property
    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();
}