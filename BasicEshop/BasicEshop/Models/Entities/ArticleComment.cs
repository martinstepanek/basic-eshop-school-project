using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class ArticleComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ArticleCommentId { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required] public string Content { get; set; }
    }
}