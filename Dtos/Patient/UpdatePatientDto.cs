using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Patient
{
    public class UpdatePatientDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters long")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}