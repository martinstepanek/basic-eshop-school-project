using System.Diagnostics;
using BasicEshop.Models;
using BasicEshop.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace BasicEshop.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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