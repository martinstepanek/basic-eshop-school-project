using System;
using System.Collections.Generic;
using System.Linq;
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
            modelBuilder.Entity<Category>()
                .HasOne(category => category.Parent)
                .WithMany()
                .HasForeignKey(category => category.ParentId);

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

            modelBuilder.Entity<Order>()
                .HasMany(order => order.Products)
                .WithOne(product => product.Order);

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            const int numberOfUsers = 10;
            const int numberOfProducts = 100;
            const int numberOfOrders = 30;
            const int numberOfArticles = 30;

            var testCustomers = new Faker<Customer>()
                .RuleFor(c => c.CustomerId, (f, c) => f.Random.Uuid().ToString())
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName())
                .RuleFor(c => c.LastName, (f, c) => f.Name.LastName())
                .RuleFor(c => c.ContactEmail, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                .RuleFor(c => c.Address, (f, c) => f.Address.StreetAddress())
                .RuleFor(c => c.City, (f, c) => f.Address.City())
                .RuleFor(c => c.Country, (f, c) => f.Address.Country())
                .RuleFor(c => c.ZipCode, (f, c) => f.Address.ZipCode());
            List<Customer> customers = testCustomers.Generate(numberOfUsers);
            modelBuilder.Entity<Customer>().HasData(customers);

            var testUsers = new Faker<User>()
                .RuleFor(u => u.UserId, (f, u) => f.Random.Uuid().ToString())
                .RuleFor(u => u.CustomerId, (f, u) => customers[f.IndexFaker].CustomerId)
                .RuleFor(u => u.Email,
                    (f, u) => f.Internet.Email(customers[f.IndexFaker].FirstName, customers[f.IndexFaker].LastName))
                .RuleFor(u => u.PasswordHash, (f, u) => f.Random.Hash());
            List<User> users = testUsers.Generate(numberOfUsers);
            modelBuilder.Entity<User>().HasData(users);

            var testCategories = new Faker<Category>()
                .RuleFor(c => c.CategoryId, (f, c) => f.Random.Uuid().ToString())
                .RuleFor(c => c.Name, (f, c) => f.Commerce.Categories(1)[0]);
            List<Category> categories = testCategories.Generate(10);
            modelBuilder.Entity<Category>().HasData(categories);

            var testSellers = new Faker<Seller>()
                .RuleFor(s => s.SellerId, (f, s) => f.Random.Uuid().ToString())
                .RuleFor(s => s.Name, (f, s) => f.Company.CompanyName());
            List<Seller> sellers = testSellers.Generate(10);
            modelBuilder.Entity<Seller>().HasData(sellers);

            var testProducts = new Faker<Product>()
                .RuleFor(p => p.ProductId, (f, p) => f.Random.Uuid().ToString())
                .RuleFor(p => p.CategoryId, (f, p) => f.PickRandom(categories).CategoryId)
                .RuleFor(p => p.SellerId, (f, p) => f.PickRandom(sellers).SellerId)
                .RuleFor(p => p.Title, (f, p) => f.Commerce.Product())
                .RuleFor(p => p.Description, (f, p) => f.PickRandom(categories).CategoryId)
                .RuleFor(p => p.Price, (f, p) => f.Finance.Amount())
                .RuleFor(p => p.PriceOld,
                    (f, p) => f.Random.Number(5) == 1 ? f.Finance.Amount(p.Price, p.Price * 2) : 0)
                .RuleFor(p => p.CreatedAt, (f, p) => f.Random.Number(3) == 1 ? f.Date.Recent() : f.Date.Past());
            List<Product> products = testProducts.Generate(numberOfProducts);
            modelBuilder.Entity<Product>().HasData(products);

            var testProductImages = new Faker<ProductImage>()
                .RuleFor(p => p.ProductImageId, (f, p) => f.Random.Uuid().ToString())
                .RuleFor(p => p.ProductId, (f, p) => f.PickRandom(products).ProductId)
                .RuleFor(p => p.FileName, (f, p) => f.Image.PicsumUrl());
            List<ProductImage> productImages = testProductImages.Generate(numberOfProducts * 6);
            modelBuilder.Entity<ProductImage>().HasData(productImages);

            var testProductUnitHistory = new Faker<ProductUnitHistory>()
                .RuleFor(p => p.ProductUnitHistoryId, (f, p) => f.Random.Uuid().ToString())
                .RuleFor(p => p.ProductId, (f, p) => f.PickRandom(products).ProductId)
                .RuleFor(p => p.Number, (f, p) => f.Random.Number(1, (int) 10e5))
                .RuleFor(p => p.Description, (f, p) => f.Lorem.Sentence());
            List<ProductUnitHistory> productUnitHistories = testProductUnitHistory.Generate(numberOfProducts * 4);
            modelBuilder.Entity<ProductUnitHistory>().HasData(productUnitHistories);

            var testProductUnitHistoryToOrder = new Faker<ProductUnitHistory>()
                .RuleFor(p => p.ProductUnitHistoryId, (f, p) => f.Random.Uuid().ToString())
                .RuleFor(p => p.ProductId, (f, p) => f.PickRandom(products).ProductId)
                .RuleFor(p => p.Number,
                    (f, p) => -1 * f.Random.Number(
                        0,
                        productUnitHistories.Where(x => x.ProductId == p.ProductId).Sum(x => x.Number)
                    ))
                .RuleFor(p => p.Description, (f, p) => "Order " + p.Number);
            List<ProductUnitHistory> productUnitHistoriesToOrder =
                testProductUnitHistoryToOrder.Generate(numberOfOrders);
            modelBuilder.Entity<ProductUnitHistory>().HasData(productUnitHistoriesToOrder);

            var testProductHasTags = new Faker<ProductHasTag>()
                .RuleFor(p => p.ProductHasTagId, (f, p) => f.Random.Uuid().ToString())
                .RuleFor(p => p.ProductId, (f, p) => f.PickRandom(products).ProductId)
                .RuleFor(p => p.Name, (f, p) => f.Random.String(4, 15));
            List<ProductHasTag> productHasTags = testProductHasTags.Generate(numberOfProducts * 3);
            modelBuilder.Entity<ProductHasTag>().HasData(productHasTags);

            var testReviews = new Faker<Review>()
                .RuleFor(r => r.ReviewId, (f, r) => f.Random.Uuid().ToString())
                .RuleFor(r => r.UserId, (f, r) => f.PickRandom(users).UserId)
                .RuleFor(r => r.ProductId, (f, r) => f.PickRandom(products).ProductId)
                .RuleFor(r => r.Stars, (f, r) => f.Random.Number(1, 5))
                .RuleFor(r => r.Content, (f, r) => f.Rant.Review());
            List<Review> reviews = testReviews.Generate(numberOfProducts * 3);
            modelBuilder.Entity<Review>().HasData(reviews);

            var testOrders = new Faker<Order>()
                .RuleFor(o => o.OrderId, (f, o) => f.Random.Uuid().ToString())
                .RuleFor(o => o.CustomerId, (f, o) => f.PickRandom(customers).CustomerId)
                .RuleFor(o => o.FirstName, (f, o) => f.Name.FirstName())
                .RuleFor(o => o.LastName, (f, o) => f.Name.LastName())
                .RuleFor(o => o.ContactEmail, (f, o) => f.Internet.Email(o.FirstName, o.LastName))
                .RuleFor(o => o.Address, (f, o) => f.Address.StreetAddress())
                .RuleFor(o => o.City, (f, o) => f.Address.City())
                .RuleFor(o => o.Country, (f, o) => f.Address.Country())
                .RuleFor(o => o.ZipCode, (f, o) => f.Address.ZipCode());
            List<Order> orders = testOrders.Generate(numberOfOrders);
            modelBuilder.Entity<Order>().HasData(orders);

            var testOrderHasProducts = new Faker<OrderHasProduct>()
                .RuleFor(o => o.OderHasProductId, (f, o) => f.Random.Uuid().ToString())
                .RuleFor(o => o.OrderId, (f, o) => orders[f.IndexFaker].OrderId)
                .RuleFor(o => o.ProductUnitHistoryId,
                    (f, o) => productUnitHistoriesToOrder[f.IndexFaker].ProductUnitHistoryId)
                .RuleFor(o => o.PriceTotal,
                    (f, o) => productUnitHistoriesToOrder[f.IndexFaker].Number * -1 * products
                        .Find(x => x.ProductId == productUnitHistoriesToOrder[f.IndexFaker].ProductId)!.Price);
            List<OrderHasProduct> orderHasProducts = testOrderHasProducts.Generate(numberOfOrders);
            modelBuilder.Entity<OrderHasProduct>().HasData(orderHasProducts);
            
            var testArticles = new Faker<Article>()
                .RuleFor(a => a.ArticleId, (f, a) => f.Random.Uuid().ToString())
                .RuleFor(a => a.UserId, (f, a) => f.PickRandom(users).UserId)
                .RuleFor(a => a.Title, (f, a) => String.Join(" ", f.Lorem.Words()))
                .RuleFor(a => a.Url, (f, a) => f.Lorem.Slug())
                .RuleFor(a => a.Content, (f, a) => f.Lorem.Paragraphs(5))
                .RuleFor(a => a.FeaturedImageFileName, (f, a) => f.Image.PicsumUrl())
                .RuleFor(a => a.PublishedAt, (f, a) => f.Date.Past());
            List<Article> articles = testArticles.Generate(numberOfArticles);
            modelBuilder.Entity<Article>().HasData(articles);

            var testArticleComments = new Faker<ArticleComment>()
                .RuleFor(a => a.ArticleCommentId, (f, a) => f.Random.Uuid().ToString())
                .RuleFor(a => a.ArticleId, (f, a) => f.PickRandom(articles).ArticleId)
                .RuleFor(a => a.UserId, (f, a) => f.PickRandom(users).UserId)
                .RuleFor(a => a.Content, (f, a) => f.Lorem.Paragraph());
            List<ArticleComment> articleComments = testArticleComments.Generate(numberOfArticles);
            modelBuilder.Entity<ArticleComment>().HasData(articleComments);
        }
    }
}