namespace peace_api.Dtos.Overview
{
    public class CardDataDto
    {
        public int AppointmentsToday { get; set; } = 0;
        public int AppointmentsTomorrow { get; set; } = 0;
        public decimal InvoicesPaidToday { get; set; } = 0;
        public decimal InvoicesPendingToday { get; set; } = 0;

    }
}