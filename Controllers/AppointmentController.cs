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
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _context.Appointments.ToListAsync();
            var appointmentDto = appointments.Select(x => x.ToAppointmentDto());

            return Ok(appointmentDto);
        }

        // GET: api/appointments/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentDto appointmentDto)
        {
            var appointment = appointmentDto.ToAppointmentFromCreateDto();
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment.ToAppointmentDto());
        }

        // PUT: api/appointments/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateAppointmentDto updateDto)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Reason = updateDto.Reason;
            appointment.AppointmentDate = updateDto.AppointmentDate;
            await _context.SaveChangesAsync();

            return Ok(appointment.ToAppointmentDto());
        }

        // DELETE: api/appointments/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}