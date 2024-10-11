namespace peace_api.Helpers
{
    public class RevenueQueryObject
    {
        public string? Year { get; set; } = null;
        public string? Month { get; set; } = null;
        public Guid? DoctorId { get; set; } = Guid.Empty;
    }
}