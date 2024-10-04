using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Doctor;
using peace_api.Mappers;

namespace peace_api.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        // GET: api/doctors
        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _context.Doctors.ToList()
             .Select(x => x.ToDoctorDto());

            return Ok(doctors);
        }

        // GET: api/doctors/:id
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.ToDoctorDto());
        }

        // POST: api/doctors
        [HttpPost]
        public IActionResult Post([FromBody] CreateDoctorDto doctorDto)
        {
            var doctor = doctorDto.ToDoctorFromCreateDto();
            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor.ToDoctorDto());
        }

        // PUT: api/doctors/:id
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdateDoctorDto updateDto)
        {
            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Name = updateDto.Name;
            _context.SaveChanges();

            return Ok(doctor.ToDoctorDto());
        }
    }
}