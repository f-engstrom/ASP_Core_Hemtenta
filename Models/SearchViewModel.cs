using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Core_Hemtenta.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Product> SearchResult { get; set; } = new List<Product>();
        public IEnumerable<Product> RecommendedProducts { get; set; } = new List<Product>();
    }
}
