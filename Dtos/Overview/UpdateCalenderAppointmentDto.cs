using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Overview
{
    public class UpdateCalenderAppointmentDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
    }
}