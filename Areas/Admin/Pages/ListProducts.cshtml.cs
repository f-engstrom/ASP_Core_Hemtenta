using System.Collections.Generic;
using System.Linq;
using ASP_Core_Hemtenta.Data;
using ASP_Core_Hemtenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP_Core_Hemtenta.Areas.Admin.Pages
{
    public class ListProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ListProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Clothing { get; set; }

        public IActionResult OnGet([Bind(Prefix = "q")] string queryString)
        {
            Clothing = _context.Products.Include(x => x.Category).ToList();

            if (queryString != null)
            {
                Clothing = _context.Products.Include(x => x.Category).Where(x => x.Name.Contains(queryString)).ToList();

            }


            if (Clothing == null)
            {
                return NotFound();
            }

            return Page();


        }


    }
}
