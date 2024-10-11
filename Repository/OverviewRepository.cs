using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Overview;
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

        public async Task<CardDataDto> GetCardDataAsync()
        {
            var today = DateTime.UtcNow.AddHours(-6).Date;
            var tomorrow = today.AddDays(1);

            var appointments = _context.Appointments.AsQueryable();
            var invoices = _context.Invoices.AsQueryable();

            var todayAppointmentsCount = await appointments.Where(a => a.AppointmentDate.Date == today).CountAsync();
            var tomorrowAppointmentsCount = await appointments.Where(a => a.AppointmentDate.Date == tomorrow).CountAsync();
            var invoiceStatus = await _context.Invoices
               .Where(i => i.CreatedAt.Date == today).ToListAsync();

            var invoicesPaidToday = invoiceStatus?.Sum(i => i.Status == Status.Paid ? i.Amount : 0) ?? 0;
            var invoicesPendingToday = invoiceStatus?.Sum(i => i.Status == Status.Pending ? i.Amount : 0) ?? 0;
            return new CardDataDto
            {
                AppointmentsToday = todayAppointmentsCount,
                AppointmentsTomorrow = tomorrowAppointmentsCount,
                InvoicesPaidToday = invoicesPaidToday,
                InvoicesPendingToday = invoicesPendingToday,
            };
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