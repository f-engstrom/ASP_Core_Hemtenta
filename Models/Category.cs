using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP_Core_Hemtenta.Models
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Image URL")]

        public string ImageUrl { get; set; }

        public  string UrlSlug { get; set; }
        private List<Product> Clothing { get; set; } = new List<Product>();
    }
}
