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
        public IActionResult GetAll()
        {
            var invoices = _context.Invoices.ToList()
             .Select(x => x.ToInvoiceDto());

            return Ok(invoices);
        }

        // GET: api/invoices/:id
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice.ToInvoiceDto());
        }

        // POST: api/invoices
        [HttpPost]
        public IActionResult Post([FromBody] CreateInvoiceDto invoiceDto)
        {
            var invoice = invoiceDto.ToInvoiceFromCreateDto();
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice.ToInvoiceDto());
        }

        // PUT: api/invoices/:id
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdateInvoiceDto updateDto)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Amount = updateDto.Amount;
            invoice.Reason = updateDto.Reason;
            invoice.Status = updateDto.Status;
            _context.SaveChanges();

            return Ok(invoice.ToInvoiceDto());
        }

        // DELETE: api/invoices/:id
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return NoContent();
        }
    }
}