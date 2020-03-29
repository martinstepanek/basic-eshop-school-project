using System.Collections.Generic;
using BasicEshop.Models.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BasicEshop.Models.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductUnitHistory> ProductUnitHistories { get; set; }
        public DbSet<ProductHasTag> ProductHasTags { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHasProduct> OrderHasProducts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
            modelBuilder.Entity<Seller>()
                .HasIndex(seller => seller.Name)
                .IsUnique();
            modelBuilder.Entity<ProductImage>()
                .HasIndex(productImage => productImage.FileName)
                .IsUnique();
            modelBuilder.Entity<ProductHasTag>()
                .HasIndex(tag => tag.Name)
                .IsUnique();
            modelBuilder.Entity<Article>()
                .HasIndex(article => article.Url)
                .IsUnique();
            modelBuilder.Entity<Article>()
                .HasIndex(article => article.FeaturedImageFileName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Articles)
                .WithOne(article => article.User);
            modelBuilder.Entity<User>()
                .HasMany(user => user.Reviews)
                .WithOne(review => review.User);

            modelBuilder.Entity<Article>()
                .HasMany(article => article.Comments)
                .WithOne(comment => comment.Article);

            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.User)
                .WithOne(user => user.Customer);

            modelBuilder.Entity<Category>()
                .HasMany(category => category.Products)
                .WithOne(product => product.Category);

            modelBuilder.Entity<Product>()
                .HasMany(product => product.Images)
                .WithOne(image => image.Product);
            modelBuilder.Entity<Product>()
                .HasMany(product => product.UnitHistories)
                .WithOne(history => history.Product);
            modelBuilder.Entity<Product>()
                .HasMany(product => product.Tags)
                .WithOne(tag => tag.Product);
            modelBuilder.Entity<Product>()
                .HasMany(product => product.Reviews)
                .WithOne(review => review.Product);

            modelBuilder.Entity<ProductUnitHistory>()
                .HasOne(history => history.OrderHasProduct)
                .WithOne(product => product.ProductUnitHistory);

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < 5; i++)
            {
                customers.Add(
                    new Faker<Customer>()
                        .RuleFor(c => c.CustomerId, (f, c) => f.Random.Uuid().ToString())
                        .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName())
                        .RuleFor(c => c.LastName, (f, c) => f.Name.LastName())
                        .RuleFor(c => c.ContactEmail, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                        .RuleFor(c => c.Address, (f, c) => f.Address.StreetAddress())
                        .RuleFor(c => c.City, (f, c) => f.Address.City())
                        .RuleFor(c => c.Country, (f, c) => f.Address.Country())
                        .RuleFor(c => c.ZipCode, (f, c) => f.Address.ZipCode())
                );
            }

            modelBuilder.Entity<Customer>().HasData(customers);
        }
    }
}