using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementService.Dtos;
using OrderManagementService.Enums;
using OrderManagementService.IManager;

namespace OrderManagementService.Controllers
{
    [Route("api/cart")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet("addToCart/{productId}")]
        public ActionResult<ResponseModel> AddItemToCart(int productId)
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var result = _cartManager.AddToCart(productId, userId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("getCart")]
        public ActionResult<ResponseModel> GetCart()
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var accessToken = Request.Headers["Authorization"];
            var result = _cartManager.GetCart(userId, accessToken);
            if (result==null)
            {
                return BadRequest();
            }
            return Ok(new ResponseModel
            {
                Data = result,
                Message = "Success"
            });
        }

        [HttpGet("placeOrder")]
        public ActionResult<ResponseModel> CheckoutCart()
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var result = _cartManager.CreateAnOrder(userId);
            if (result == 0)
            {
                return BadRequest();
            }
            return new ResponseModel
            {
                Data = result,
                Message = "Success"
            };
        }

        [HttpPost("markAsPaid")]
        public IActionResult MakePaymentForOrder([FromBody]OrderPayment orderPayment)
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var result = _cartManager.MarkOrderAsPlaced(orderPayment.OrderId);
            return Ok(new ResponseModel { Data = result, Message = "Success" });

        }

        [HttpGet("getOrders")]
        public ActionResult<ResponseModel> GetOrdersByUserId()
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var accessToken = Request.Headers["Authorization"];
            var result = _cartManager.GetOrdersForUser(userId, accessToken);
            if (result == null)
            {
                return BadRequest();
            }
            return new ResponseModel
            {
                Data = result,
                Message = "Success"
            };
        }

        [HttpGet("markOrderAsDelivered/{orderId}")]
        public ActionResult<ResponseModel> MarkOrderAsDelivered(int orderId)
        {
            var result = _cartManager.UpdateOrderDeliveryStatus(orderId,(int)OrderStatus.Delivered);
            return Ok(new ResponseModel { Data = result, Message = "Success" });
        }

        [HttpGet("markOrderAsUndelivered/{orderId}")]
        public ActionResult<ResponseModel> MarkOrderAsUnDelivered(int orderId)
        {
            var result = _cartManager.UpdateOrderDeliveryStatus(orderId, (int)OrderStatus.UnDelivered);
            return Ok(new ResponseModel { Data = result, Message = "Success" });
        }

        
    }
}