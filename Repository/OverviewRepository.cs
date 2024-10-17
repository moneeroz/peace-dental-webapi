using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Overview;
using peace_api.Dtos.Revenue;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Repository
{
    public class OverviewRepository(ApplicationDBContext context) : IOverviewRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<List<Appointment>> GetCalenderDataAsync()
        {
            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6).AddHours(-6).Date;
            var appointments = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Where(a => a.AppointmentDate.Date >= sixMonthsAgo).AsQueryable();

            return await appointments.ToListAsync();
        }

        public async Task<InvoiceStatsDto> GetTodayInvoiceStatsAsync()
        {
            var today = DateTime.UtcNow.AddHours(-6).Date;

            var result = await _context.Invoices.Where(i => i.CreatedAt.Date == today)
                .GroupBy(a => a.Status)
                .Select(a => new
                {
                    Status = a.Key,
                    Amount = a.Sum(b => b.Amount),
                    Count = a.Count()
                })
                .ToListAsync();

            return new InvoiceStatsDto
            {
                Paid = result.Where(a => a.Status == Status.Paid).Sum(a => a.Amount),
                Pending = result.Where(a => a.Status == Status.Pending).Sum(a => a.Amount),
                Count = result.Sum(a => a.Count)
            };
        }

        public async Task<int> GetTodayAppointmentCountAsync()
        {
            var today = DateTime.UtcNow.AddHours(-6).Date;

            return await _context.Appointments.Where(a => a.AppointmentDate.Date == today).CountAsync();
        }

        public async Task<Appointment?> UpdateAppointment(UpdateCalenderAppointmentDto appointmentDto, Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return null;
            }

            appointment.AppointmentDate = appointmentDto.AppointmentDate;

            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}