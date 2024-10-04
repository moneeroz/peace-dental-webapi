using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Patient;
using peace_api.Mappers;

namespace peace_api.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        // GET: api/patients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _context.Patients.ToListAsync();
            var patientDto = patients.Select(x => x.ToPatientDto());

            return Ok(patientDto);
        }

        // GET: api/patients/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

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
            var patient = patientDto.ToPatientFromCreateDto();
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient.ToPatientDto());
        }

        // PUT: api/patients/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdatePatientDto updateDto)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            patient.Name = updateDto.Name;
            patient.Phone = updateDto.Phone;
            await _context.SaveChangesAsync();

            return Ok(patient.ToPatientDto());
        }

        // DELETE: api/patients/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}