using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Shared.Dto
{
    [Serializable]
    public class OrderPayment
    {
        public int OrderId { get; set; }
        public decimal Cost { get; set; }
    }
}
