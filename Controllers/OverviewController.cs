using Microsoft.AspNetCore.Mvc;
using peace_api.Dtos.Appointment;
using peace_api.Dtos.Overview;
using peace_api.Dtos.Revenue;
using peace_api.Interfaces;
using peace_api.Mappers;
using peace_api.Models;

namespace peace_api.Controllers
{
    [Route("api/overview")]
    [ApiController]
    public class OverviewController(IOverviewRepository overviewRepo) : ControllerBase
    {
        private readonly IOverviewRepository _overviewRepo = overviewRepo;

        // GET: api/overview/invoice-stats
        [HttpGet("invoice-stats")]
        public async Task<IActionResult> GetInvoiceStats()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            InvoiceStatsDto? invoiceStats = await _overviewRepo.GetTodayInvoiceStatsAsync();

            return Ok(invoiceStats);
        }

        // GET: api/overview/appointment-count
        [HttpGet("appointment-count")]
        public async Task<IActionResult> GetAppointmentCount()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int appointmentsCount = await _overviewRepo.GetTodayAppointmentCountAsync();

            return Ok(appointmentsCount);
        }

        // GET: api/overview/calender
        [HttpGet("calender")]
        public async Task<IActionResult> GetCalenderData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Appointment>? appointments = await _overviewRepo.GetCalenderDataAsync();

            IEnumerable<AppointmentDto>? appointmentDto = appointments.Select(x => x.ToAppointmentDto());

            return Ok(appointmentDto);
        }

        // PUT: api/overview/appointment/:id
        [HttpPut("update-appointment/{id:guid}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, [FromBody] UpdateCalenderAppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Appointment? appointment = await _overviewRepo.UpdateAppointment(appointmentDto, id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }
    }
}