using System.ComponentModel.DataAnnotations;

namespace peace_api.Dtos.Appointment
{
    public class CreateAppointmentDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Reason must be at least 4 characters long")]
        [MaxLength(255, ErrorMessage = "Reason must be at most 255 characters long")]
        public string Reason { get; set; } = string.Empty;
        [Required]
        public Guid DoctorId { get; set; }
    }
}