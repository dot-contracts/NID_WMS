using System.ComponentModel.DataAnnotations;

namespace wms_android.shared.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)] // Enforce minimum password length
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; } // Optional, or make required

        public string LastName { get; set; } // Optional, or make required

        public string? RoleName { get; set; } // Optional: to assign a role during registration
                                            // Alternatively, you could pass a RoleId
    }
}
