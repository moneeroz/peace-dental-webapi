using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;

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

            return Ok(invoices);
        }

        // GET: api/invoices/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }
    }
}