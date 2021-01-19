using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ASP_Core_Hemtenta.Data;
using ASP_Core_Hemtenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Core_Hemtenta.Areas.Admin.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Categories { get; set; }
        public ProductDTO ProductDto = new ProductDTO();

        public IActionResult OnGet()
        {
            Categories = _context.Categories.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList();

            return Page();


        }


        public IActionResult OnPost(ProductDTO productDto)
        {

            Product clothing = new Product();

            string urlSulg = productDto.Name.ToLower().Replace(" ", "-");

            clothing.Name = productDto.Name;
            clothing.Description = productDto.Description;
            clothing.Price = productDto.Price;
            clothing.ImageUrl = productDto.ImageUrl;
            clothing.UrlSlug = urlSulg;
            clothing.CategoryId = productDto.CategoryId;
            clothing.ClimateCompensated = productDto.ClimateCompensated;

            _context.Add(clothing);
            _context.SaveChanges();

            return RedirectToPage("ListProducts");

        }

        
        public class ClothingDTO
        {
            public string Name { get; set; }

            public string Description { get; set; }

            [Column(TypeName = "decimal(19,4)")]
            public decimal Price { get; set; }

            [DataType(DataType.ImageUrl)]
            [Display(Name = "Image URL")]
            public string ImageUrl { get; set; }

            public int CategoryId { get; set; }
            public Category Category { get; set; }

            [Display(Name = "Climate Compensated")]
            public bool ClimateCompensated { get; set; }

        }

    }
}