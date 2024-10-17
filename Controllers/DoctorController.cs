using Microsoft.AspNetCore.Mvc;
using peace_api.Data;
using peace_api.Dtos.Doctor;
using peace_api.Interfaces;
using peace_api.Mappers;
using peace_api.Models;

namespace peace_api.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController(ApplicationDBContext context, IDoctorRepository doctorRepo) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IDoctorRepository _doctorRepo = doctorRepo;

        // GET: api/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Doctor>? doctors = await _doctorRepo.GetAllAsync();

            IEnumerable<DoctorDto>? doctorDto = doctors.Select(x => x.ToDoctorDto());

            return Ok(doctorDto);
        }

        // GET: api/doctors/:id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Doctor? doctor = await _doctorRepo.GetByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.ToDoctorDto());
        }

        // POST: api/doctors
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDoctorDto doctorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Doctor? doctor = doctorDto.ToDoctorFromCreateDto();

            await _doctorRepo.CreateAsync(doctor);

            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor.ToDoctorDto());
        }

        // PUT: api/doctors/:id
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateDoctorDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Doctor? doctor = await _doctorRepo.UpdateAsync(id, updateDto);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.ToDoctorDto());
        }

        // DELETE: api/doctors/:id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Doctor? doctor = await _doctorRepo.DeleteAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}