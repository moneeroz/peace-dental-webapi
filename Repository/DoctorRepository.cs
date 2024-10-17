using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Doctor;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Repository
{
    public class DoctorRepository(ApplicationDBContext context) : IDoctorRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctor?> DeleteAsync(Guid id)
        {
            Doctor? doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return null;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<Doctor?> UpdateAsync(Guid id, UpdateDoctorDto DoctorDto)
        {
            Doctor? existingDoctor = await _context.Doctors.FindAsync(id);

            if (existingDoctor == null)
            {
                return null;
            }

            existingDoctor.Name = DoctorDto.Name;

            await _context.SaveChangesAsync();

            return existingDoctor;
        }
    }
}
