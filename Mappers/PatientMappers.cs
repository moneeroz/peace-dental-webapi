using peace_api.Dtos.Patient;
using peace_api.Models;

namespace peace_api.Mappers
{
    public static class PatientMappers
    {
        public static PatientDto ToPatientDto(this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone,
                InvoiceCount = patient.Invoices.Count,
                TotalPaid = patient.Invoices.Where(i => i.Status == Status.Paid).Sum(i => i.Amount),
                TotalPending = patient.Invoices.Where(i => i.Status == Status.Pending).Sum(i => i.Amount)
            };
        }

        public static Patient ToPatientFromCreateDto(this CreatePatientDto patientDto)
        {
            return new Patient
            {
                Name = patientDto.Name,
                Phone = patientDto.Phone
            };
        }
    }
}