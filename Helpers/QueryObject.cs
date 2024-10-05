using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Helpers
{
    public class QueryObject
    {
        // filter params
        public string? PatientName { get; set; } = null;
        public string? DoctorName { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string? Reason { get; set; } = null;

        // pagination params
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }
}