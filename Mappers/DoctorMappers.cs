using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Dtos.Doctor;
using peace_api.Models;

namespace peace_api.Mappers
{
    public static class DoctorMappers
    {
        public static DoctorDto ToDoctorDto(this Doctor doctor)
        {
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name
            };
        }
    }
}