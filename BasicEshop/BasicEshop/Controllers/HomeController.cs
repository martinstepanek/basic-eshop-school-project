using System.Diagnostics;
using System.Linq;
using BasicEshop.Models;
using BasicEshop.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicEshop.Controllers
{
    public class HomeController : BaseController
    {
        private DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Products = _context.Products
                .Include(x => x.Images)
                .ThenInclude(image => image.Image)
                .Take(8)
                .ToList();

            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}