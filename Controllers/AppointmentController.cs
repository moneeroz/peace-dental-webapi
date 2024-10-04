using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Appointment;
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
        public IActionResult GetById([FromRoute] Guid id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }

        // POST: api/appointments
        [HttpPost]
        public IActionResult Post([FromBody] CreateAppointmentDto appointmentDto)
        {
            var appointment = appointmentDto.ToAppointmentFromCreateDto();
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment.ToAppointmentDto());
        }
    }
}