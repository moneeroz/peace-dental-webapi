using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Patient;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Repository
{
    public class PatientRepository(ApplicationDBContext context) : IPatientRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Patient> CreateAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient?> DeleteAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return null;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<bool> PatientExists(Guid id)
        {
            return await _context.Patients.AnyAsync(x => x.Id == id);
        }

        public async Task<Patient?> UpdateAsync(Guid id, UpdatePatientDto patientDto)
        {
            var existingPatient = await _context.Patients.FindAsync(id);

            if (existingPatient == null)
            {
                return null;
            }

            existingPatient.Name = patientDto.Name;
            existingPatient.Phone = patientDto.Phone;
            await _context.SaveChangesAsync();

            return existingPatient;
        }
    }
}