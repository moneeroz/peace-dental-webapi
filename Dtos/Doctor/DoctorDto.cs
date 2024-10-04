using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Doctor
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}