using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Patient;
using peace_api.Helpers;
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
            Patient? patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return null;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<List<Patient>> GetAllAsync(QueryObject query)
        {
            IQueryable<Patient>? patients = _context.Patients.Include(a => a.Invoices).AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                string? term = query.term.ToLower();
                patients = patients.Where(a => a.Name.ToLower().Contains(term) ||
                            a.Phone.Contains(term));
            }

            // Sort results
            patients = patients.OrderBy(a => a.Name);

            // Pagination
            int offset = (query.Page - 1) * query.PageSize;
            int limit = query.PageSize;

            return await patients.Skip(offset).Take(limit).ToListAsync();
        }
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            IQueryable<Patient>? patients = _context.Patients.AsQueryable();

            // Sort results
            patients = patients.OrderBy(a => a.Name);


            return await patients.ToListAsync();
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
            Patient? existingPatient = await _context.Patients.FindAsync(id);

            if (existingPatient == null)
            {
                return null;
            }

            existingPatient.Name = patientDto.Name;
            existingPatient.Phone = patientDto.Phone;
            await _context.SaveChangesAsync();

            return existingPatient;
        }

        public async Task<int> GetPageCountAsync(QueryObject query)
        {
            IQueryable<Patient>? patients = _context.Patients.Include(a => a.Invoices).AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                string? term = query.term.ToLower();
                patients = patients.Where(a => a.Name.ToLower().Contains(term) ||
                            a.Phone.Contains(term));
            }

            int totalItems = await patients.CountAsync();
            int itemsPerPage = query.PageSize;

            return (int)Math.Ceiling((double)totalItems / itemsPerPage);
        }

        public async Task<List<Invoice>> GetPatientInvoicesAsync(Guid id, QueryObject query)
        {
            IQueryable<Invoice>? invoices = _context.Invoices.Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                string? term = query.term.ToLower();
                invoices = invoices.Where(a => a.Patient.Name.ToLower().Contains(term) ||
                             a.Doctor.Name.ToLower().Contains(term) ||
                             a.Reason.ToLower().Contains(term));
            }

            // Sort results
            invoices = invoices.OrderBy(a => a.CreatedAt);

            // Pagination
            int offset = (query.Page - 1) * query.PageSize;
            int limit = query.PageSize;

            return await invoices.Where(a => a.PatientId == id).Skip(offset).Take(limit).ToListAsync();
        }
    }
}