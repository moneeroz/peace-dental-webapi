using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace peace_api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [PersonalData]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
    }
}