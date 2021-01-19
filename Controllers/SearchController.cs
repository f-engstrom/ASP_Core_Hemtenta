using System.Linq;
using System.Threading.Tasks;
using ASP_Core_Hemtenta.Data;
using ASP_Core_Hemtenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Core_Hemtenta.Controllers
{
    public class SearchController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index([Bind(Prefix = "q")] string queryString)
        {
            SearchViewModel viewModel = new SearchViewModel();

            viewModel.RecommendedProducts = _context.Products.Take(4).ToList();
            viewModel.SearchResult = _context.Products.Where(x => x.Name.Contains(queryString)).ToList();
            return View(viewModel);
        }
    }
}