using Microsoft.AspNetCore.Mvc;
using peace_api.Data;
using peace_api.Dtos.Invoice;
using peace_api.Dtos.Patient;
using peace_api.Helpers;
using peace_api.Interfaces;
using peace_api.Mappers;
using peace_api.Models;

namespace peace_api.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController(ApplicationDBContext context, IPatientRepository patientRepo) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IPatientRepository _patientRepo = patientRepo;

        // GET: api/patients
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Patient>? patients = await _patientRepo.GetAllAsync(query);

            IEnumerable<PatientDto>? patientDto = patients.Select(x => x.ToPatientDto());

            return Ok(patientDto);
        }

        // GET: api/patients/:id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Patient? patient = await _patientRepo.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient.ToPatientDto());
        }

        // POST: api/patients
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePatientDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Patient? patient = patientDto.ToPatientFromCreateDto();

            await _patientRepo.CreateAsync(patient);

            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient.ToPatientDto());
        }

        // PUT: api/patients/:id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdatePatientDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Patient? patient = await _patientRepo.UpdateAsync(id, updateDto);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient.ToPatientDto());
        }

        // DELETE: api/patients/:id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Patient? patient = await _patientRepo.DeleteAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/patients/count
        [HttpGet("count")]
        public async Task<IActionResult> GetCount([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int count = await _patientRepo.GetPageCountAsync(query);

            return Ok(count);
        }

        // GET: api/patients/invoices
        [HttpGet("invoices/{id:guid}")]
        public async Task<IActionResult> GetPatientInvoices([FromRoute] Guid id, [FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Invoice>? invoices = await _patientRepo.GetPatientInvoicesAsync(id, query);

            IEnumerable<InvoiceDto>? invoiceDto = invoices.Select(x => x.ToInvoiceDto());

            return Ok(invoiceDto);
        }

        // GET: api/patients/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPatients()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Patient>? patients = await _patientRepo.GetAllPatientsAsync();

            IEnumerable<PatientDto>? patientDto = patients.Select(x => x.ToPatientDto());

            return Ok(patientDto);
        }
    }
}