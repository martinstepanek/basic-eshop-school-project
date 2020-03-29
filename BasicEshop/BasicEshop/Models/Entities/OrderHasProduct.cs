using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class OrderHasProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OderHasProductId { get; set; }

        [ForeignKey("OrderId")] [Required] public Order Order { get; set; }

        public string ProductUnitHistoryId { get; set; }
        public ProductUnitHistory ProductUnitHistory { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal PriceTotal { get; set; }
    }
}