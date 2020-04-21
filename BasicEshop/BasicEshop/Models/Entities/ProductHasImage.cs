using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class ProductHasImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductHasImageId { get; set; }

        [Required] public string ProductId { get; set; }
        public Product Product { get; set; }
        [Required] public string ImageId { get; set; }
        public Image Image { get; set; }
    }
}