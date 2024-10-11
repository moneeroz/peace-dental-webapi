using System.ComponentModel.DataAnnotations.Schema;

namespace peace_api.Models
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AppointmentDate { get; set; }
        public Guid? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}