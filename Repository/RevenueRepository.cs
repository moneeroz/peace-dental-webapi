using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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

        public async Task<RevenueCardDto> GetCardDataAsync(RevenueQueryObject query)
        {
            var year = int.Parse(query?.Year ?? DateTime.UtcNow.Year.ToString());

            var invoices = _context.Invoices.Where(a => a.CreatedAt.Year == year).AsQueryable();

            if (query?.Month != null)
            {
                invoices = invoices.Where(a => a.CreatedAt.Month == int.Parse(query.Month));
            }

            if (query?.DoctorId != Guid.Empty && query?.DoctorId != null)
            {
                invoices = invoices.Where(a => a.DoctorId == query.DoctorId);
            }

            var groupedInvoices = await invoices.GroupBy(a => a.Status).ToListAsync();

            var totalPaid = groupedInvoices.Where(a => a.Key == Status.Paid).Sum(a => a.Sum(b => b.Amount));

            var totalPending = groupedInvoices.Where(a => a.Key == Status.Pending).Sum(a => a.Sum(a => a.Amount));

            var numberOfPatients = await _context.Patients.Where(a => a.CreatedAt.Year == year).CountAsync();

            var numberOfInvoices = await invoices.CountAsync();

            return new RevenueCardDto
            {
                NumberOfPatients = numberOfPatients,
                NumberOfInvoices = numberOfInvoices,
                TotalPaid = totalPaid,
                TotalPending = totalPending
            };
        }

        public async Task<List<RevenueChartDto>> GetChartDataAsync(RevenueQueryObject query)
        {
            var year = int.Parse(query?.Year ?? DateTime.UtcNow.Year.ToString());

            var invoices = _context.Invoices.Where(a => a.CreatedAt.Year == year).OrderByDescending(a => a.CreatedAt.Month).AsQueryable();

            var res = invoices.GroupBy(a => a.CreatedAt.Month).Select(a => new
            {
                Month = ConvertMonth(a.Key),
                Paid = a.Sum(i => i.Status == Status.Paid ? i.Amount : 0),
                Pending = a.Sum(i => i.Status == Status.Pending ? i.Amount : 0)
            });

            var results = await res.ToListAsync();

            var revenueDto = results.Select(a => new RevenueChartDto
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
            var invoices = await _context.Invoices.Include(a => a.Doctor).Include(a => a.Patient).OrderByDescending(a => a.CreatedAt).Take(5).ToListAsync();

            var data = invoices.Select(a => new LatestInvoiceDto
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