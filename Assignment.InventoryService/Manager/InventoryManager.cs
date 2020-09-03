using Assignment.InventoryService.IManager;
using Assignment.InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.InventoryService.Manager
{
    public class InventoryManager : IInventoryManager
    {
        private static List<Product> inventory = new List<Product>();
        private static int maxId = 0;
        private readonly ILogManager _logManager;

        public InventoryManager(ILogManager logManager)
        {
            _logManager = logManager;
        }
        public void AddProduct(Product product)
        {
            try
            {             
                maxId++;
                product.Id = maxId;
                _logManager.LogDebug($"Saving product in inventory with Id: {product.Id}");
                inventory.Add(product);
            }
            catch(Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                _logManager.LogDebug($"Deleting product from inventory with Id: { productId}");
                var productToBeDeleted = inventory.FirstOrDefault(i => i.Id == productId);
                if (productToBeDeleted == null)
                {
                    return false;
                }
                inventory.Remove(productToBeDeleted);
                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                return false; //throw here
            }
        }

        public IEnumerable<Product> GetProducts(string name)           //may be size or anything is also search criteria //filtering as in trace,make post rquest
        {
            try
            {
                _logManager.LogDebug($"search products by product name: {name}");
                var productsSearched = inventory.Where(i => i.Name.Contains(name));
                return productsSearched;  //need to put further check on what is to be returned,add dto here
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                throw new Exception();
            }
        }

        public Product GetProductById(int productId)           //may be size or anything is also search criteria //filtering as in trace,make post rquest
        {
            try
            {
                _logManager.LogDebug($"get product by id: {productId}");
                var product = inventory.FirstOrDefault(i => i.Id == productId);
                return product;  //need to put further check on what is to be returned,add dto here
            }
            catch (Exception ex)
            {
                _logManager.LogExceptionAndInnerException(ex);
                throw new Exception();
            }
        }
    }
}
