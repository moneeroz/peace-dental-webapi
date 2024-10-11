namespace peace_api.Dtos.Patient
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int InvoiceCount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalPending { get; set; }
    }
}