namespace EnterpriseInventory.Domain.Entities;

using EnterpriseInventory.Domain.Common;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}