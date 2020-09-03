using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderManagementService.Dtos;
using OrderManagementService.Enums;
using OrderManagementService.IManager;
using OrderManagementService.IService;
using OrderManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementService.Manager
{
    public class CartManager:ICartManager
    {
        private static List<Cart> carts = new List<Cart>();  //how to distinguish bw orders and carts  //can lock cart may be //or only some items are moved to order check this
        private static int maxCartId=0;
        private static List<Order> orders = new List<Order>();  //how to distinguish bw orders and carts  //can lock cart may be //or only some items are moved to order check this
        private static int maxOrderId = 0;
        private readonly ILogManager _logManager;
        private readonly IConfiguration _configuration;
        private readonly IFetchProductDataService _fetchProductDataService;

        public CartManager(ILogManager logManager,IConfiguration configuration,IFetchProductDataService fetchProductDataService)
        {
            _logManager = logManager;
            _configuration = configuration;
            _fetchProductDataService = fetchProductDataService;
        }

        public bool AddToCart(int productId,int userId)
        {
            try
            {
                _logManager.LogDebug($"Add product to cart the product with Id: {productId} for user : {userId}");
                var cartForUser = carts.FirstOrDefault(c => c.UserId == userId);
                if (cartForUser == null)
                {
                    maxCartId++;
                    cartForUser = new Cart { UserId = userId, CartId = maxCartId, CartItems = new List<CartItem>() };
                    carts.Add(cartForUser);
                }
                var itemInCart = cartForUser.CartItems.FirstOrDefault(c => c.ProductId == productId);
                if (itemInCart == null)
                {
                    addItemtoCart(productId, userId, cartForUser);
                }
                else
                {
                    updateItemInCart(itemInCart);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                return false;
            }
        }

        public Cart GetCart(int userId,string accessToken)
        {
            _logManager.LogDebug($"Get cart for user : {userId}");
            var cart = carts.FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                cart.CartItems = fetchCartItems(cart.CartItems, accessToken);
            }
            return cart;
        }

        public void RemoveAllCartItems(int userId)
        {
            try
            {
                _logManager.LogDebug($"Empty the cart for user: {userId}");
                var cartForUser = carts.FirstOrDefault(c => c.UserId == userId);
                if (cartForUser == null)
                {
                }
                cartForUser.CartItems = new List<CartItem>();
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
            }
        }

        public void RemoveCartItemById(int productId,int userId)  //check if need to remove this by product id or cart id
        {
            _logManager.LogDebug($"Remove an item from cart with id {productId} by user {userId}");
            var cartForUser = carts.FirstOrDefault(c => c.UserId == userId);
            if (cartForUser == null)
            {
               
            }
            var cartItemToBeRemoved = cartForUser.CartItems.FirstOrDefault(c => c.ProductId == productId);
            if (cartItemToBeRemoved == null)
            {

            }
            cartForUser.CartItems.Remove(cartItemToBeRemoved);
        }

        public IEnumerable<Order> GetOrdersToBeDelivered()
        {
            _logManager.LogInfo($"Fetching orders to be delivered");
            var ordersToBeDelivered = orders.Where(o => o.OrderStatus == (int)OrderStatus.Placed);
            return ordersToBeDelivered;
        }

        public bool UpdateOrderDeliveryStatus(int orderId,int deliveryStatus)   //whether we want to have one way or not
        {
            _logManager.LogDebug($"Update order delivery status for order : {orderId} to {deliveryStatus}");
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null || order.OrderStatus ==(int)OrderStatus.Pending)
            {
                return false;
            }
            order.OrderStatus = deliveryStatus;
            return true;
        }

        public bool MarkOrderAsPlaced(int orderId)
        {
            _logManager.LogDebug($"Update status after payment for order : {orderId}");
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return false;  
            }
            order.OrderStatus = (int)OrderStatus.Placed;
            return true;
        }

        public int CreateAnOrder(int userId)
        {
            try
            {
                _logManager.LogDebug($"creating an order for user: {userId}");
                var cartForUser = carts.FirstOrDefault(c => c.UserId == userId);
                if (cartForUser == null || !cartForUser.CartItems.Any())
                {
                    return 0;
                }
                maxOrderId++;
                orders.Add(new Order { OrderId = maxOrderId, OrderedItems = cartForUser.CartItems, OrderStatus = (int)OrderStatus.Pending, UserId = userId }); //need to remove this from here only
                cartForUser = new Cart();
                return maxOrderId;
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                return 0;
            }
        }

        public IEnumerable<Order> GetOrdersForUser(int userId,string accessToken)
        {
            _logManager.LogDebug($"fetching orders for user: {userId}");
            var ordersForUser = orders.Where(o => o.UserId == userId);
            if (orders.Any())
            {
                foreach(var order in orders)
                {
                    order.OrderedItems = fetchCartItems(order.OrderedItems, accessToken);
                }
            }
            return ordersForUser;
        }

        private void addItemtoCart(int productId,int quantity,Cart cart)
        {
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = quantity
            };
            cart.CartItems.Add(cartItem); 
        }

        private void updateItemInCart(CartItem cartItem)
        {
            cartItem.Quantity++;  
        }

        private void deleteItemFromCart(CartItem cartItem)
        {

        }

        private List<CartItem> fetchCartItems(List<CartItem> cartItems,string accessToken)
        {
            _logManager.LogDebug($"fetching products info");
            foreach(var item in cartItems)
            {
                var product = fetchProductData(item.ProductId, accessToken).Result;
                if (product != null)
                {
                    item.ProductName = product.Name;
                    item.Cost = product.Price;
                }
                
            }
            return cartItems;
        }

        private async Task<Product> fetchProductData(int productId,string accessToken)
        {
            _logManager.LogDebug($"fetching products info");
            var apiUrl = _configuration.GetValue<string>("GetProduct") + productId;
            var responseMessage = await _fetchProductDataService.GetDataFromOrderManagementService(apiUrl, accessToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(response);
                return product;
            }
            return null;
        }



    }
}
