using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Core_Hemtenta.Data;
using ASP_Core_Hemtenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Core_Hemtenta.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(string slug)
        {
            List<Product> productsForCategory = new List<Product>();


            productsForCategory = _context.Products.Where(x => x.Category.UrlSlug == slug).Include(c => c.Category).ToList();
            return View(productsForCategory);
        }
    }
}