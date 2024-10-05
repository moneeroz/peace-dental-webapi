using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Appointment;
using peace_api.Helpers;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync(QueryObject query);
        Task<Appointment?> GetByIdAsync(Guid id); // can be null
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> UpdateAsync(Guid id, UpdateAppointmentDto appointmentDto);
        Task<Appointment?> DeleteAsync(Guid id);
    }
}