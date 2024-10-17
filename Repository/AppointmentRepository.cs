using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Appointment;
using peace_api.Helpers;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Repository
{
    public class AppointmentRepository(ApplicationDBContext context) : IAppointmentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment?> DeleteAsync(Guid id)
        {
            Appointment? appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return null;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<List<Appointment>> GetAllAsync(QueryObject query)
        {
            IQueryable<Appointment>? appointments = _context.Appointments.Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                string? term = query.term.ToLower();
                appointments = appointments.Where(a => a.Patient.Name.ToLower().Contains(term) ||
                            a.Patient.Phone.Contains(term) ||
                            a.Doctor.Name.ToLower().Contains(term) ||
                            a.AppointmentDate.ToString().Contains(term) ||
                            a.Reason.ToLower().Contains(term));
            }

            // Sort results
            appointments = appointments.OrderByDescending(a => a.AppointmentDate);

            // Pagination
            int offset = (query.Page - 1) * query.PageSize;
            int limit = query.PageSize;


            return await appointments.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments.Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Appointment?> UpdateAsync(Guid id, UpdateAppointmentDto appointmentDto)
        {
            Appointment? existingAppointment = await _context.Appointments.FindAsync(id);

            if (existingAppointment == null)
            {
                return null;
            }

            existingAppointment.AppointmentDate = appointmentDto.AppointmentDate;
            existingAppointment.DoctorId = appointmentDto.DoctorId;
            existingAppointment.Reason = appointmentDto.Reason;

            await _context.SaveChangesAsync();

            return existingAppointment;
        }

        public async Task<int> GetPageCountAsync(QueryObject query)
        {
            IQueryable<Appointment>? appointments = _context.Appointments.Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            // Check for query term and filter the results
            if (!string.IsNullOrWhiteSpace(query.term))
            {
                string? term = query.term.ToLower();
                appointments = appointments.Where(a => a.Patient.Name.ToLower().Contains(term) ||
                            a.Patient.Phone.Contains(term) ||
                            a.Doctor.Name.ToLower().Contains(term) ||
                            a.AppointmentDate.ToString().Contains(term) ||
                            a.Reason.ToLower().Contains(term));
            }

            int totalItems = await appointments.CountAsync();
            int itemsPerPage = query.PageSize;

            return (int)Math.Ceiling((double)totalItems / itemsPerPage);
        }
    }
}