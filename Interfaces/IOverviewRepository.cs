using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Overview;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface IOverviewRepository
    {
        Task<CardDataDto> GetCardDataAsync();
        Task<List<Appointment>> GetCalenderDataAsync();
        Task<Appointment?> UpdateAppointment(UpdateCalenderAppointmentDto appointmentDto, Guid id);
    }
}