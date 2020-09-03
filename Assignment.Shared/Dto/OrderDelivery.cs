using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Shared.Dto
{
    [Serializable]
    public class OrderDelivery
    {
        public int OrderId { get; set; }
        public int DeliveryStatus { get; set; }
    }
}
