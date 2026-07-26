namespace EnterpriseInventory.Application.Authorization.Constants;

/// <summary>
/// Defines custom JWT claim types used throughout the application.
///
/// These constants eliminate magic strings and ensure that the same
/// claim names are used consistently when generating and validating JWTs.
/// </summary>
public static class ClaimConstants
{
    /// <summary>
    /// Represents the custom JWT claim type used to store
    /// application permissions.
    ///
    /// Example:
    /// Type  : permission
    /// Value : Product.Create
    /// </summary>
    public const string Permission = "permission";
}