using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaymentManagementService.Dtos;
using PaymentManagementService.IManager;

namespace PaymentManagementService.Controllers
{
    [Route("api/payment")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;
        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        [HttpPost("makePayment")]
        public IActionResult MakePaymentForOrder([FromBody]OrderPayment orderPayment)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            var accessToken = Request.Headers["Authorization"];
            var paymentResult = _paymentManager.MakePayment(orderPayment,accessToken).Result;  //check for userId
            if (paymentResult.Data)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("test")]
        public IActionResult Test()   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            return Ok();
        }
    }
}