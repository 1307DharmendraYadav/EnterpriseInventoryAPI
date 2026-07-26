namespace EnterpriseInventory.Application.Authorization;

public static class PermissionConstants
{
    public static class Product
    {
        public const string View = "Product.View";
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";
    }

    public static class User
    {
        public const string View = "User.View";
        public const string Create = "User.Create";
        public const string Update = "User.Update";
        public const string Delete = "User.Delete";
    }
}