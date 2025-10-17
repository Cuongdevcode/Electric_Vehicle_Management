using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Fullname { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key Relationship about Role
        [Required]
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;

    }
}
