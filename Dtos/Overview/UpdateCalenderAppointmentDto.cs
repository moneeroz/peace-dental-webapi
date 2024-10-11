using System.ComponentModel.DataAnnotations;

namespace peace_api.Dtos.Overview
{
    public class UpdateCalenderAppointmentDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
    }
}