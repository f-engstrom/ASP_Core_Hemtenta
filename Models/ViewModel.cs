using System.Collections.Generic;

namespace ASP_Core_Hemtenta.Models
{
    public class ViewModel
    {

        public IEnumerable<Product> Clothing { get; set; }
        public IEnumerable<Category> Category { get; set; }

        public Product ClothingItem { get; set; }

    }

}

