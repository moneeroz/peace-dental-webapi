using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Invoice
{
    public class CreateInvoiceDto
    {
        [Required]
        [Range(1, 1000000, ErrorMessage = "Amount must be between 1 and 1000000")]
        public decimal Amount { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Reason must be at least 4 characters long")]
        [MaxLength(255, ErrorMessage = "Reason must be at most 255 characters long")]
        public string Reason { get; set; } = string.Empty;
        [Required]
        public Guid DoctorId { get; set; }
    }
}