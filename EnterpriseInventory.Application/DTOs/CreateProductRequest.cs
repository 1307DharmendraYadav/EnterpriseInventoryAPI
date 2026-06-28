using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseInventory.Application.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
