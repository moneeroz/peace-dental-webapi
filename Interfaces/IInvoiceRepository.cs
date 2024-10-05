using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Invoice;
using peace_api.Helpers;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync(QueryObject query);
        Task<Invoice?> GetByIdAsync(Guid id); // can be null
        Task<Invoice> CreateAsync(Invoice invoice);
        Task<Invoice?> UpdateAsync(Guid id, UpdateInvoiceDto invoiceDto);
        Task<Invoice?> DeleteAsync(Guid id);
    }
}