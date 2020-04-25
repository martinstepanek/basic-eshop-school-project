using System.Linq;
using System.Threading.Tasks;
using BasicEshop.Models;
using BasicEshop.Models.Database;
using BasicEshop.Models.Entities;
using BasicEshop.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicEshop.Controllers
{
    public class ProductController : BaseController
    {
        private DatabaseContext _context;

        public ProductController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, [FromQuery]ProductFilterForm productFilterForm = null)
        {
            IQueryable<Product> products = _context.Products
                .Include(x => x.Images)
                .ThenInclude(image => image.Image);

            if (productFilterForm.CategoryId != null)
            {
                products = products.Where(x => x.CategoryId == productFilterForm.CategoryId);
            }

            if (productFilterForm.SellerIds != null)
            {
                var sellerIds = productFilterForm.SellerIds.Split(",").ToList();
                products = products.Where(x => sellerIds.Contains(x.SellerId));
            }
            
            ViewBag.Products = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber, 9);

            ViewBag.Categories = _context.Categories.Where(x => x.ParentId == null).ToList();
            ViewBag.Sellers = _context.Sellers.ToList();
            return View(productFilterForm);
        }

        public IActionResult? Search()
        {
            return null;
        }
    }
}