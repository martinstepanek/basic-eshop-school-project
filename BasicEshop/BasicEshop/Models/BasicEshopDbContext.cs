using Microsoft.EntityFrameworkCore;

namespace BasicEshop.Models
{
    public class BasicEshopDbContext : DbContext
    {
        public BasicEshopDbContext(DbContextOptions<BasicEshopDbContext> options) : base(options)
        {
        }
    }
}