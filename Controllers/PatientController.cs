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
        public IActionResult GetAll()
        {
            var patients = _context.Patients.ToList()
             .Select(x => x.ToPatientDto());

            return Ok(patients);
        }

        // GET: api/patients/:id
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var patient = _context.Patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient.ToPatientDto());
        }

        // POST: api/patients
        [HttpPost]
        public IActionResult Post([FromBody] CreatePatientDto patientDto)
        {
            var patient = patientDto.ToPatientFromCreateDto();
            _context.Patients.Add(patient);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient.ToPatientDto());
        }

        // PUT: api/patients/:id
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdatePatientDto updateDto)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.Name = updateDto.Name;
            patient.Phone = updateDto.Phone;
            _context.SaveChanges();

            return Ok(patient.ToPatientDto());
        }

        // DELETE: api/patients/:id
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var patient = _context.Patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return NoContent();
        }
    }
}