using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Service1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentQuanitity { get; set; }
        public decimal Price { get; set; }
    }
}
