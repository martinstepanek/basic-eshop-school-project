using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BasicEshop.Models;

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
            _context.Users.Add(new User());
            _context.SaveChanges();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}