using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Invoice
{
    public class CreateInvoiceDto
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public Guid DoctorId { get; set; }
    }
}