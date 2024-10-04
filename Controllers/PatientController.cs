using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
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
        public IActionResult GetById(Guid id)
        {
            var patient = _context.Patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient.ToPatientDto());
        }
    }
}