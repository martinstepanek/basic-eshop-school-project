using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEshop.Models.Entities
{
    public class User
    {
        public enum UserAccess
        {
            User,
            Admin
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        public UserAccess Access { get; set; } = UserAccess.User;
        public ICollection<Article> Articles { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}