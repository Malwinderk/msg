using Assignment.InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.InventoryService.IManager
{
    public interface IInventoryManager
    {
        void AddProduct(Product product);
        bool DeleteProduct(int productId);
        IEnumerable<Product> GetProducts(string name);
        Product GetProductById(int productId);
    }
}
