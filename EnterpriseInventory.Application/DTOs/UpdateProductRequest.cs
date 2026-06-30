using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseInventory.Application.DTOs
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
