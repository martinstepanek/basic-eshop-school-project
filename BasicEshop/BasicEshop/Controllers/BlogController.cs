using System;
using System.Linq;
using System.Threading.Tasks;
using BasicEshop.Models;
using BasicEshop.Models.Database;
using BasicEshop.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicEshop.Controllers
{
    public class BlogController : BaseController
    {
        private DatabaseContext _context;

        public BlogController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var articles = _context.Articles
                .Include(article => article.FeaturedImage)
                .Include(article => article.User.Customer)
                .Where(article => article.PublishedAt != null)
                .Where(article => article.PublishedAt < DateTime.Now)
                .OrderByDescending(article => article.PublishedAt);
            ViewBag.Articles = await PaginatedList<Article>.CreateAsync(articles.AsNoTracking(), pageNumber);

            return View();
        }

        public IActionResult Detail(string articleUrl)
        {
            ViewBag.Article = _context.Articles
                .Include(article => article.FeaturedImage)
                .Include(article => article.User.Customer)
                .Include(article => article.Comments)
                .ThenInclude(comment => comment.User.Customer)
                .Where(article => article.PublishedAt < DateTime.Now)
                .Where(article => article.PublishedAt != null)
                .FirstOrDefault(article => article.Url == articleUrl);

            return View();
        }
    }
}