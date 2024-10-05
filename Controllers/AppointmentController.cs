using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Appointment;
using peace_api.Interfaces;
using peace_api.Mappers;

namespace peace_api.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController(ApplicationDBContext context, IAppointmentRepository appointmentRepo, IPatientRepository patientRepo) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IAppointmentRepository _appointmentRepo = appointmentRepo;
        private readonly IPatientRepository _patientRepo = patientRepo;

        // GET: api/appointments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentRepo.GetAllAsync();

            var appointmentDto = appointments.Select(x => x.ToAppointmentDto());

            return Ok(appointmentDto);
        }

        // GET: api/appointments/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }

        // POST: api/appointments
        [HttpPost("{patientId}")]
        public async Task<IActionResult> Post([FromRoute] Guid patientId, [FromBody] CreateAppointmentDto appointmentDto)
        {
            if (!await _patientRepo.PatientExists(patientId))
            {
                return BadRequest("Patient does not exist");
            }

            var appointment = appointmentDto.ToAppointmentFromCreateDto(patientId);

            await _appointmentRepo.CreateAsync(appointment);

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment.ToAppointmentDto());
        }

        // PUT: api/appointments/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateAppointmentDto updateDto)
        {
            var appointment = await _appointmentRepo.UpdateAsync(id, updateDto);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }

        // DELETE: api/appointments/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var appointment = await _appointmentRepo.DeleteAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}