using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CategoryId { get; set; }

        public string ParentId { get; set; }
        public Category Parent { get; set; }

        [Required] public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}