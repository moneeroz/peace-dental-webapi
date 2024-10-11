using System.ComponentModel.DataAnnotations;

namespace peace_api.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters long")]
        public string Name { get; set; } = string.Empty;
    }
}