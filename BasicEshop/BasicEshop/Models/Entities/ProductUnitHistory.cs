using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class ProductUnitHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductUnitHistoryId { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        [Required] public int Number { get; set; }
        [Required] public string Description { get; set; }

        public OrderHasProduct OrderHasProduct { get; set; }
    }
}