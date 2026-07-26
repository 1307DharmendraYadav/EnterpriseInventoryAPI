namespace EnterpriseInventory.Application.Common.Settings;

public sealed class DefaultAdminSettings
{
    public const string SectionName = "DefaultAdmin";

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}