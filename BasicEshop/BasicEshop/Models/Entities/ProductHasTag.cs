using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class ProductHasTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductHasTagId { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        [Required] public string Name { get; set; }
    }
}