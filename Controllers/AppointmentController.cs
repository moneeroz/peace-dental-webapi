using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;

namespace peace_api.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        // GET: api/appointments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _context.Appointments.ToListAsync();

            return Ok(appointments);
        }

        // GET: api/appointments/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }
    }
}