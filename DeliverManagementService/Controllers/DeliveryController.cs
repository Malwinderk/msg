using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryManagementService.IManager;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DeliveryManagementService.Controllers
{
    [Authorize]
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryManager _deliveryManager;

        public DeliveryController(IDeliveryManager deliveryManager)
        {
            _deliveryManager = deliveryManager;
        }

        [HttpGet("markDelivered/{orderId}")]
        public IActionResult MarkOrderAsDelivered(int orderId)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            var accessToken = Request.Headers["Authorization"];
            var deliveryResult = _deliveryManager.MarkOrderAsDelivered(orderId, accessToken).Result;  //check for userId
            if (deliveryResult.Data)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("markUndelivered/{orderId}")]
        public IActionResult MarkOrderAsUndelivered(int orderId)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            var accessToken = Request.Headers["Authorization"];
            var deliveryResult = _deliveryManager.MarkOrderAsUndelivered(orderId, accessToken).Result;  //check for userId
            if (deliveryResult.Data)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}