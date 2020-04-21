using Microsoft.AspNetCore.Mvc;

namespace BasicEshop.Controllers
{
    public class BlogController : BaseController
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}