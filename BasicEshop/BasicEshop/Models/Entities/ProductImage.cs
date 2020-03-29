using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductImageId { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        [Required] public string FileName { get; set; }
    }
}