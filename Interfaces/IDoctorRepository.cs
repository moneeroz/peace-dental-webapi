using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Doctor;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(Guid id); // can be null
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor?> UpdateAsync(Guid id, UpdateDoctorDto doctorDto);
        Task<Doctor?> DeleteAsync(Guid id);
    }
}