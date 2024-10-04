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
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        // GET: api/appointments
        [HttpGet]
        public IActionResult GetAll()
        {
            var appointments = _context.Appointments.ToList()
             .Select(x => x.ToAppointmentDto());

            return Ok(appointments);
        }

        // GET: api/appointments/:id
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }
    }
}