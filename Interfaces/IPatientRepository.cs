using peace_api.Dtos.Patient;
using peace_api.Helpers;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync(QueryObject query);
        Task<Patient?> GetByIdAsync(Guid id); // can be null
        Task<Patient> CreateAsync(Patient patient);
        Task<Patient?> UpdateAsync(Guid id, UpdatePatientDto patientDto);
        Task<Patient?> DeleteAsync(Guid id);

        Task<bool> PatientExists(Guid id);

        Task<int> GetPageCountAsync(QueryObject query);

        Task<List<Invoice>> GetPatientInvoicesAsync(Guid id, QueryObject query);
        Task<List<Patient>> GetAllPatientsAsync();
    }
}