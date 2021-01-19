using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Core_Hemtenta.Models
{
    public class Basket
    {

        public Dictionary<Product,int> ProductBasket { get; set; } = new Dictionary<Product, int>();

        public decimal Total { get; set; }

    }
}
