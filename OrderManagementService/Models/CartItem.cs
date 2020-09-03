using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementService.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }  //need to keep price here or fetch from db?
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
    }
}
