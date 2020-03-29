using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderHasProduct> Products { get; set; }
    }
}