using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Invoice;
using peace_api.Mappers;

namespace peace_api.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        // GET: api/invoices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _context.Invoices.ToListAsync();
            var invoiceDto = invoices.Select(x => x.ToInvoiceDto());

            return Ok(invoiceDto);
        }

        // GET: api/invoices/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice.ToInvoiceDto());
        }

        // POST: api/invoices
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInvoiceDto invoiceDto)
        {
            var invoice = invoiceDto.ToInvoiceFromCreateDto();
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice.ToInvoiceDto());
        }

        // PUT: api/invoices/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateInvoiceDto updateDto)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Amount = updateDto.Amount;
            invoice.Reason = updateDto.Reason;
            invoice.Status = updateDto.Status;
            await _context.SaveChangesAsync();

            return Ok(invoice.ToInvoiceDto());
        }

        // DELETE: api/invoices/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}