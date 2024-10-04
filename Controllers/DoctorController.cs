using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;

namespace peace_api.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        // GET: api/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _context.Doctors.ToListAsync();

            return Ok(doctors);
        }

        // GET: api/doctors/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }
    }
}