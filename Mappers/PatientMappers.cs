using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Phone = patient.Phone
            };
        }
    }
}