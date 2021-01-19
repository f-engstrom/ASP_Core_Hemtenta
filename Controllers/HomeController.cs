using System.Linq;
using System.Threading.Tasks;
using ASP_Core_Hemtenta.Data;
using Microsoft.AspNetCore.Mvc;
using ASP_Core_Hemtenta.Models;

namespace ASP_Core_Hemtenta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.Clothing = _context.Products.ToList();
            viewModel.Category = _context.Categories.ToList();
            return View(viewModel);
        }
    }
}
