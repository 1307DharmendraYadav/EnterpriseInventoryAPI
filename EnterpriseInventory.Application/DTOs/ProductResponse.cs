using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseInventory.Application.DTOs;

public class ProductResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
