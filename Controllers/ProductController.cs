using System.Linq;
using System.Threading.Tasks;
using ASP_Core_Hemtenta.Data;
using ASP_Core_Hemtenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Core_Hemtenta.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string slug)
        {

            if (slug == null)
            {
                return NotFound();
            }

            ViewModel viewModel = new ViewModel();
            viewModel.Clothing = _context.Products.ToList();

            viewModel.ClothingItem = await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.UrlSlug == slug);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
