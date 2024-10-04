using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Patient
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}