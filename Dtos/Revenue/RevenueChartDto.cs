using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Revenue
{
    public class RevenueChartDto
    {
        public string Name { get; set; } = string.Empty;
        public List<SeriesItem> Series { get; set; } = [];
    }

    public class SeriesItem
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}