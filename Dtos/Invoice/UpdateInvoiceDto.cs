using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Models;

namespace peace_api.Dtos.Invoice
{
    public class UpdateInvoiceDto
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public Status Status { get; set; }
    }
}