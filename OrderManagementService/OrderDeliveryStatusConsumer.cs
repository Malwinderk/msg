using MassTransit;
using OrderManagementService.Dtos;
using OrderManagementService.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementService
{
    [Serializable]
    public class OrderDeliveryStatusConsumer: IConsumer<OrderDelivery>
    {
        private readonly ICartManager _cartManager;

        public OrderDeliveryStatusConsumer(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }
        public Task Consume(ConsumeContext<OrderDelivery> context)
        {
            _cartManager.UpdateOrderDeliveryStatus(context.Message.OrderId, context.Message.DeliveryStatus);
            return Task.CompletedTask;
        }
    }
}
