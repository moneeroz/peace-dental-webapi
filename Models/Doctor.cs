using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Models
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<Appointment> Appointments { get; } = [];
        public ICollection<Invoice> Invoices { get; } = [];
    }
}