namespace peace_api.Dtos.Revenue
{
    public class InvoiceStatsDto
    {
        public decimal Paid { get; set; } = 0;
        public decimal Pending { get; set; } = 0;
        public int Count { get; set; } = 0;

    }
}