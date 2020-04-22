using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerId { get; set; }

        public User User { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string ContactEmail { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }
        [Required] public string ZipCode { get; set; }


        public string FullName => FirstName + " " + LastName;
    }
}