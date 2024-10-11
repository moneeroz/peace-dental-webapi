using Microsoft.AspNetCore.Mvc;
using peace_api.Dtos.Overview;
using peace_api.Interfaces;
using peace_api.Mappers;

namespace peace_api.Controllers
{
    [Route("api/overview")]
    [ApiController]
    public class OverviewController(IOverviewRepository overviewRepo) : ControllerBase
    {
        private readonly IOverviewRepository _overviewRepo = overviewRepo;

        // GET: api/overview
        [HttpGet("card-data")]
        public async Task<IActionResult> GetCardData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cardData = await _overviewRepo.GetCardDataAsync();

            return Ok(cardData);
        }

        // GET: api/overview/calender
        [HttpGet("calender")]
        public async Task<IActionResult> GetCalenderData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointments = await _overviewRepo.GetCalenderDataAsync();

            var appointmentDto = appointments.Select(x => x.ToAppointmentDto());

            return Ok(appointmentDto);
        }

        // PUT: api/overview/appointment/:id
        [HttpPut("update-appointment/{id:guid}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, [FromBody] UpdateCalenderAppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = await _overviewRepo.UpdateAppointment(appointmentDto, id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment.ToAppointmentDto());
        }
    }
}