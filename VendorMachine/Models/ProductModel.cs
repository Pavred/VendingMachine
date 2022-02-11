
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VendorMachine.Models
{
    public class ProductModel
    {
        public int ProductId { get; set;}
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
                    
    }
}