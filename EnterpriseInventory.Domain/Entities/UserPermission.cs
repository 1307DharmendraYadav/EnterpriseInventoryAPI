using EnterpriseInventory.Domain.Common;

namespace EnterpriseInventory.Domain.Entities;

public class UserPermission : BaseEntity
{
    public int UserId { get; set; }

    public int PermissionId { get; set; }

    /// <summary>
    /// true  = Explicit Allow
    /// false = Explicit Deny
    /// </summary>
    public bool IsAllowed { get; set; }

    // Navigation Properties

    public User User { get; set; } = null!;

    public Permission Permission { get; set; } = null!;
}