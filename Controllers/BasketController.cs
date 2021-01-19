using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Core_Hemtenta.Data;
using ASP_Core_Hemtenta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ASP_Core_Hemtenta.Controllers
{
    public class BasketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BasketController(ApplicationDbContext context)
        {
            _context = context;
        }

        Basket basket = new Basket();
        Product product = new Product();


        public ActionResult Index()
        {

            if (HttpContext.Session.Keys.Count() > 0)
            {


                foreach (var slug in HttpContext.Session.Keys)
                {
                    var amountOfProductItems = HttpContext.Session.GetInt32(slug);


                    if (amountOfProductItems != null)
                    {


                        product = _context.Products.FirstOrDefault(x => x.UrlSlug == slug);


                        basket.ProductBasket.Add(product, (int)amountOfProductItems);


                    }

                }

                foreach (var product in basket.ProductBasket)
                {


                    basket.Total += (product.Value * product.Key.Price);

                }


                return View(basket);

            }

            return View();

        }

        public IActionResult Add(string slug)
        {
            int amount;
            var currentAmount = HttpContext.Session.GetInt32(slug);
            if (currentAmount == null)
            {

                HttpContext.Session.SetInt32(slug, 1);
                return RedirectToAction("Details", "Product", new RouteValueDictionary(
                    new { controller = "Product", action = "Details", slug = slug }));

            }

            amount = (int)(currentAmount + 1);

            HttpContext.Session.SetInt32(slug, amount);

            return RedirectToAction("Details", "Product", new RouteValueDictionary(
               new { controller = "Product", action = "Details", slug = slug }));

        }


    }
}