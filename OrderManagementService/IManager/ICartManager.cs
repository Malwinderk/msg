using OrderManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementService.IManager
{
    public interface ICartManager
    {
        bool AddToCart(int productId, int userId);
        Cart GetCart(int userId,string accessToken);
        void RemoveCartItemById(int productId, int userId);
        IEnumerable<Order> GetOrdersToBeDelivered();
        bool UpdateOrderDeliveryStatus(int orderId, int deliveryStatus);
        bool MarkOrderAsPlaced(int orderId);
        int CreateAnOrder(int userId);
        IEnumerable<Order> GetOrdersForUser(int userId,string accessToken);
    }
}
