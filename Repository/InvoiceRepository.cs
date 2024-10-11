using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Invoice;
using peace_api.Helpers;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Repository
{
    public class InvoiceRepository(ApplicationDBContext context) : IInvoiceRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<Invoice?> DeleteAsync(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return null;
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<List<Invoice>> GetAllAsync(QueryObject query)
        {
            var invoices = _context.Invoices.Include(a => a.Patient).Include(a => a.Doctor).AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                var term = query.term.ToLower();
                invoices = invoices.Where(a => a.Patient.Name.ToLower().Contains(term) ||
                            a.Doctor.Name.ToLower().Contains(term) ||
                            a.Reason.ToLower().Contains(term));
            }

            // Sort results
            invoices = invoices.OrderByDescending(a => a.CreatedAt);

            // Pagination
            var offset = (query.Page - 1) * query.PageSize;
            var limit = query.PageSize;

            return await invoices.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Invoices.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Invoice?> UpdateAsync(Guid id, UpdateInvoiceDto invoiceDto)
        {
            var existingInvoice = await _context.Invoices.FindAsync(id);

            if (existingInvoice == null)
            {
                return null;
            }

            existingInvoice.Amount = invoiceDto.Amount;
            existingInvoice.Status = invoiceDto.Status;
            existingInvoice.Reason = invoiceDto.Reason;

            await _context.SaveChangesAsync();

            return existingInvoice;
        }

        public async Task<int> GetPageCountAsync(QueryObject query)
        {
            var invoices = _context.Invoices.Include(a => a.Patient).Include(a => a.Doctor).AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                var term = query.term.ToLower();
                invoices = invoices.Where(a => a.Patient.Name.ToLower().Contains(term) ||
                            a.Doctor.Name.ToLower().Contains(term) ||
                            a.Reason.ToLower().Contains(term));
            }

            var totalItems = await invoices.CountAsync();
            var itemsPerPage = query.PageSize;

            return (int)Math.Ceiling((double)totalItems / itemsPerPage);
        }
    }
}