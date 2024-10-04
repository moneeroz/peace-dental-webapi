using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Invoice;
using peace_api.Models;

namespace peace_api.Mappers
{
    public static class InvoiceMappers
    {
        public static InvoiceDto ToInvoiceDto(this Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                Amount = invoice.Amount,
                Status = invoice.Status == Status.Paid ? "Paid" : "Pending",
                Reason = invoice.Reason
            };
        }
    }
}