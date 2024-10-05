using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Helpers
{
    public class QueryObject
    {
        public string? PatientName { get; set; } = null;
        public string? DoctorName { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string? Reason { get; set; } = null;
    }
}