using peace_api.Dtos.Overview;
using peace_api.Dtos.Revenue;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface IOverviewRepository
    {
        Task<InvoiceStatsDto> GetTodayInvoiceStatsAsync();
        Task<int> GetTodayAppointmentCountAsync();
        Task<List<Appointment>> GetCalenderDataAsync();
        Task<Appointment?> UpdateAppointment(UpdateCalenderAppointmentDto appointmentDto, Guid id);
    }
}