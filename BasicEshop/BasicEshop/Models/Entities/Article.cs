using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ArticleId { get; set; }

        [Required] public string UserId { get; set; }
        public User User { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Url { get; set; }
        [Required] public string Content { get; set; }
        [Required] public string FeaturedImageId { get; set; }
        public Image FeaturedImage { get; set; }
        public DateTime PublishedAt { get; set; }

        public ICollection<ArticleComment> Comments { get; set; }
    }
}