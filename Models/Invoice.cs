using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Models
{
    public enum Status
    {
        Paid,
        Pending
    }
    public class Invoice
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; } = 0;
        public Status Status { get; set; } = Status.Pending;
        [Column(TypeName = "varchar(255)")]
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}