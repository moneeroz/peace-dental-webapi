using Microsoft.AspNetCore.Mvc;
using peace_api.Data;
using peace_api.Dtos.Invoice;
using peace_api.Helpers;
using peace_api.Interfaces;
using peace_api.Mappers;
using peace_api.Models;

namespace peace_api.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController(ApplicationDBContext context, IInvoiceRepository invoiceRepo, IPatientRepository patientRepo) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IInvoiceRepository _invoiceRepo = invoiceRepo;
        private readonly IPatientRepository _patientRepo = patientRepo;

        // GET: api/invoices
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Invoice>? invoices = await _invoiceRepo.GetAllAsync(query);

            IEnumerable<InvoiceDto>? invoiceDto = invoices.Select(x => x.ToInvoiceDto());

            return Ok(invoiceDto);
        }

        // GET: api/invoices/:id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Invoice? invoice = await _invoiceRepo.GetByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice.ToInvoiceDto());
        }

        // POST: api/invoices
        [HttpPost("{patientId:guid}")]
        public async Task<IActionResult> Post([FromRoute] Guid patientId, [FromBody] CreateInvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _patientRepo.PatientExists(patientId))
            {
                return BadRequest("Patient does not exist");
            }

            Invoice? invoice = invoiceDto.ToInvoiceFromCreateDto(patientId);

            await _invoiceRepo.CreateAsync(invoice);

            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice.ToInvoiceDto());
        }

        // PUT: api/invoices/:id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateInvoiceDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Invoice? invoice = await _invoiceRepo.UpdateAsync(id, updateDto);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice.ToInvoiceDto());
        }

        // DELETE: api/invoices/:id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Invoice? invoice = await _invoiceRepo.DeleteAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/invoices/count
        [HttpGet("count")]
        public async Task<IActionResult> GetCount([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int count = await _invoiceRepo.GetPageCountAsync(query);

            return Ok(count);
        }
    }
}