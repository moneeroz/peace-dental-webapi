using Microsoft.AspNetCore.Mvc;
using peace_api.Data;
using peace_api.Dtos.Appointment;
using peace_api.Helpers;
using peace_api.Interfaces;
using peace_api.Mappers;
using peace_api.Models;

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
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            List<Appointment>? appointments = await _appointmentRepo.GetAllAsync(query);

            IEnumerable<AppointmentDto>? appointmentDto = appointments.Select(x => x.ToAppointmentDto());

            return Ok(appointmentDto);
        }

        // GET: api/appointments/:id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Appointment? appointment = await _appointmentRepo.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }

        // POST: api/appointments
        [HttpPost("{patientId:guid}")]
        public async Task<IActionResult> Post([FromRoute] Guid patientId, [FromBody] CreateAppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _patientRepo.PatientExists(patientId))
            {
                return BadRequest("Patient does not exist");
            }

            Appointment? appointment = appointmentDto.ToAppointmentFromCreateDto(patientId);

            await _appointmentRepo.CreateAsync(appointment);

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment.ToAppointmentDto());
        }

        // PUT: api/appointments/:id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateAppointmentDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Appointment? appointment = await _appointmentRepo.UpdateAsync(id, updateDto);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }

        // DELETE: api/appointments/:id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Appointment? appointment = await _appointmentRepo.DeleteAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/appointments/count
        [HttpGet("count")]
        public async Task<IActionResult> GetCount([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int count = await _appointmentRepo.GetPageCountAsync(query);

            return Ok(count);
        }
    }
}