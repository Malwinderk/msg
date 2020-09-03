using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.InventoryService.Dtos;
using Assignment.InventoryService.IManager;
using Assignment.InventoryService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.InventoryService.Controllers
{
    [Authorize]
    [Route("api/inventory")]
    public class InventoryController : Controller
    {
        private readonly IInventoryManager _inventoryManager;

        public InventoryController(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("product/add")]
        public ActionResult<ResponseModel> AddProduct([FromBody]Product product)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            _inventoryManager.AddProduct(product);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("product/delete/{productId}")]               //check if want to make it delete type
        public ActionResult<ResponseModel> DeleteProduct(int productId)   //add dto here to protect the id //but no need in a way,since we may have to share id anyway 
        {
            var success = _inventoryManager.DeleteProduct(productId);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("product/{productName}")]
        public ActionResult<ResponseModel> GetProductsByName(string productName)
        {
            var result = _inventoryManager.GetProducts(productName);
            return new ResponseModel
            {
                Data = result,
                Message = "Success"
            };
        }

        [HttpGet("productById/{productId}")]
        public ActionResult<Product> GetProductById(int productId)
        {
            var product = _inventoryManager.GetProductById(productId);
            if (product == null)
            {
                return BadRequest();
            }
            return product;
        }
    }
}