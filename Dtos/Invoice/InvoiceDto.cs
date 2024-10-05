using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Invoice
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime Date { get; set; }

    }
}