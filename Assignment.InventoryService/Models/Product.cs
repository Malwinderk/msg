using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.InventoryService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentQuanitity { get; set; }
        public decimal Price { get; set; }
    }
}
