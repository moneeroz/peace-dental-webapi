using Microsoft.EntityFrameworkCore;
using peace_api.Data;
using peace_api.Dtos.Revenue;
using peace_api.Helpers;
using peace_api.Interfaces;
using peace_api.Models;

namespace peace_api.Repository
{
    public class RevenueRepository(ApplicationDBContext context) : IRevenueRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<InvoiceStatsDto> GetInvoiceStatsAsync(RevenueQueryObject query)
        {
            int year = int.Parse(query?.Year ?? DateTime.UtcNow.Year.ToString());

            IQueryable<Invoice>? invoices = _context.Invoices.AsQueryable()
                .Where(a => a.CreatedAt.Year == year);

            if (!string.IsNullOrWhiteSpace(query?.Month))
            {
                invoices = invoices.Where(a => a.CreatedAt.Month == int.Parse(query.Month));
            }

            if (query?.DoctorId != Guid.Empty && query?.DoctorId != null)
            {
                invoices = invoices.Where(a => a.DoctorId == query.DoctorId);
            }

            var result = await invoices.GroupBy(a => a.Status)
                .Select(a => new
                {
                    Status = a.Key,
                    Amount = a.Sum(b => b.Amount),
                    Count = a.Count()
                })
                .ToListAsync();

            return new InvoiceStatsDto
            {
                Paid = result.Where(a => a.Status == Status.Paid).Sum(a => a.Amount),
                Pending = result.Where(a => a.Status == Status.Pending).Sum(a => a.Amount),
                Count = result.Sum(a => a.Count)
            };
        }

        public async Task<int> GetPatientCountAsync(RevenueQueryObject query)
        {
            int year = int.Parse(query?.Year ?? DateTime.UtcNow.Year.ToString());
            int numberOfPatients = await _context.Patients.Where(a => a.CreatedAt.Year == year).CountAsync();

            return numberOfPatients;
        }

        public async Task<List<RevenueChartDto>> GetChartDataAsync(RevenueQueryObject query)
        {
            int year = int.Parse(query?.Year ?? DateTime.UtcNow.Year.ToString());

            IQueryable<Invoice>? invoices = _context.Invoices.Where(a => a.CreatedAt.Year == year)
                .OrderByDescending(a => a.CreatedAt.Month)
                .AsQueryable();

            var groupedInvoices = invoices.GroupBy(a => a.CreatedAt.Month).Select(a => new
            {
                Month = ConvertMonth(a.Key),
                Paid = a.Sum(i => i.Status == Status.Paid ? i.Amount : 0),
                Pending = a.Sum(i => i.Status == Status.Pending ? i.Amount : 0)
            });

            var results = await groupedInvoices.ToListAsync();

            List<RevenueChartDto>? revenueDto = results.Select(a => new RevenueChartDto
            {
                Name = a.Month,
                Series =
                [
                    new SeriesItem
                    {
                        Name = "Paid",
                        Value = a.Paid
                    },
                    new SeriesItem
                    {
                        Name = "Pending",
                        Value = a.Pending
                    }
                ]
            }).ToList();


            return revenueDto;


        }

        public async Task<List<LatestInvoiceDto>> GetLatestInvoicesAsync()
        {
            List<Invoice>? invoices = await _context.Invoices.Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderByDescending(a => a.CreatedAt)
                .Take(5)
                .ToListAsync();

            List<LatestInvoiceDto>? data = invoices.Select(a => new LatestInvoiceDto
            {
                Id = a.Id,
                Amount = a.Amount,
                DoctorName = a.Doctor.Name,
                PatientName = a.Patient.Name,
                PhoneNumber = a.Patient.Phone
            }).ToList();

            return data;

        }

        private static string ConvertMonth(int month)
        {
            // Convert month number to month name (1 = January, 12 = December)
            return new DateTime(1, month, 1).ToString("MMMM");
        }
    }
}