using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ImageId { get; set; }

        public string UserId { get; set; }

        [Required] public User User { get; set; }

        [Required] public string FileName { get; set; }
    }
}