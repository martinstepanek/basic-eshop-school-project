using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BasicEshop.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string SellerId { get; set; }
        public Seller Seller { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(13,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "DECIMAL(13,2)")] public decimal PriceOld { get; set; }
        [Required] public DateTime CreatedAt { get; set; }

        public ICollection<ProductHasImage> Images { get; set; }
        public ICollection<ProductUnitHistory> UnitHistories { get; set; }
        public ICollection<ProductHasTag> Tags { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public Image? FeaturedImage => Images != null && Images.Count > 0 ? Images.FirstOrDefault()?.Image : null;

        public bool IsDiscounted => PriceOld != 0 && PriceOld != Price;

        public string? SpecialTag => CreatedAt > DateTime.Today.AddMonths(-3) ? "New" : null;
    }
}