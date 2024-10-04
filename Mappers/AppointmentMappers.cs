using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Appointment;
using peace_api.Models;

namespace peace_api.Mappers
{
    public static class AppointmentMappers
    {
        public static AppointmentDto ToAppointmentDto(this Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                Reason = appointment.Reason,
                AppointmentDate = appointment.AppointmentDate,
                DoctorName = appointment.Doctor?.Name ?? string.Empty,
                PatientName = appointment.Patient?.Name ?? string.Empty,
                PhoneNumber = appointment.Patient?.Phone ?? string.Empty
            };
        }
    }
}