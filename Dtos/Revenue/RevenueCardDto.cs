namespace peace_api.Dtos.Revenue
{
    public class RevenueCardDto
    {
        public int NumberOfPatients { get; set; } = 0;
        public int NumberOfInvoices { get; set; } = 0;
        public decimal TotalPaid { get; set; } = 0;
        public decimal TotalPending { get; set; } = 0;
    }
}