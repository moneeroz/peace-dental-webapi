using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Helpers
{
    public class RevenueQueryObject
    {
        public string? Year { get; set; } = null;
        public string? Month { get; set; } = null;
        public Guid? DoctorId { get; set; } = Guid.Empty;
    }
}