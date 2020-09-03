using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Service1.Models;
using Assignment.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Service1.Controllers
{
    [Route("api/inventory")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost("product/add")]
        public IActionResult AddProduct([FromBody]Product product)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            var inventoryList = new List<Product>();  //need to fetch id every time
            inventoryList.Add(product);
            return Ok();
        }

        [HttpGet("product/delete")]
        public IActionResult DeleteProduct(int productId)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            var inventoryList = new List<Product>();  //need to fetch id every time
            var product = inventoryList.FirstOrDefault(p => p.Id == productId);
            inventoryList.Remove(product);
            return Ok();
        }
    }
}