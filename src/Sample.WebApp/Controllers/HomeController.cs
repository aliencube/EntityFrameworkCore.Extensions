using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductDbContext _context;

        public HomeController(ProductDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        public IActionResult Index()
        {
            var product = new Product()
                          {
                              Name = $"Product-{DateTimeOffset.Now:yyyyMMddHHmmss}",
                              ProductPrices =
                              {
                                  new ProductPrice()
                                  {
                                      Value = 100.00M,
                                      ValidFrom = DateTimeOffset.Now.AddDays(1),
                                      ValidTo = DateTimeOffset.Now.AddDays(2)
                                  }
                              }
                          };

            this._context.Products.Add(product);
            this._context.SaveChanges();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
