﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementService.Dtos
{
    public class OrderDelivery
    {
        public int OrderId { get; set; }
        public int DeliveryStatus { get; set; }
    }
}
