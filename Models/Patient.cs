using System.ComponentModel.DataAnnotations.Schema;

namespace peace_api.Models
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "varchar(255)")]
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<Appointment> Appointments { get; } = [];
        public ICollection<Invoice> Invoices { get; } = [];
    }
}