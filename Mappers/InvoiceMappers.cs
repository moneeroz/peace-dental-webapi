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
                Status = invoice.Status,
                Reason = invoice.Reason,
                DoctorName = invoice.Doctor?.Name ?? string.Empty,
                PatientName = invoice.Patient?.Name ?? string.Empty,
                Date = invoice.CreatedAt
            };
        }

        public static Invoice ToInvoiceFromCreateDto(this CreateInvoiceDto invoiceDto, Guid patientId)
        {
            return new Invoice
            {
                Amount = invoiceDto.Amount,
                Reason = invoiceDto.Reason,
                PatientId = patientId,
                DoctorId = invoiceDto.DoctorId,
                Status = invoiceDto.Status
            };
        }
    }
}