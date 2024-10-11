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

        public static Doctor ToDoctorFromCreateDto(this CreateDoctorDto doctorDto)
        {
            return new Doctor
            {
                Name = doctorDto.Name
            };
        }
    }
}