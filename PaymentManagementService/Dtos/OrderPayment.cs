using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagementService.Dtos
{
    public class OrderPayment
    {
        public int OrderId { get; set; }
        public decimal Cost { get; set; }
    }
}
