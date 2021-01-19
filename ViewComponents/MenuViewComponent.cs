using System.Threading.Tasks;
using ASP_Core_Hemtenta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Core_Hemtenta.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string menuName)
        {
            var menu = await _context.Menus.Include(x => x.MenuItems).
                FirstOrDefaultAsync(x => x.Name == menuName);

            return View(menu);
        }
    }
}
