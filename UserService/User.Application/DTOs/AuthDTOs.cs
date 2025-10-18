using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTOs
{
    public class AuthDTOs
    {

        public class RegisterDto
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
            public string Password { get; set; }

            [Required]
            public string FullName { get; set; }

            
        }
        public class LoginRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
        public class LoginResponse
        {
            public Guid UserId { get; set; }

        
            public string Email { get; set; }
            public string Role { get; set; }
            public string Token { get; set; }
        }
    }
}
