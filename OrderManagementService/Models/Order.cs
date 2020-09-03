using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementService.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public List<CartItem> OrderedItems { get; set; }
        public int UserId { get; set; }
    }
}
