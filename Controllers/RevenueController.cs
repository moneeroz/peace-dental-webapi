using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using peace_api.Dtos.Revenue;
using peace_api.Helpers;
using peace_api.Interfaces;

namespace peace_api.Controllers
{
    [Route("api/revenue")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class RevenueController(IRevenueRepository revenueRepo) : ControllerBase
    {
        private readonly IRevenueRepository _revenueRepo = revenueRepo;

        // GET: api/revenue/invoice-stats
        [HttpGet("invoice-stats")]
        public async Task<IActionResult> GetInvoiceStats([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            InvoiceStatsDto? invoiceStats = await _revenueRepo.GetInvoiceStatsAsync(query);

            return Ok(invoiceStats);
        }

        // GET: api/revenue/patient-count
        [HttpGet("patient-count")]
        public async Task<IActionResult> GetPatientCount([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int patientCount = await _revenueRepo.GetPatientCountAsync(query);

            return Ok(patientCount);
        }

        // Get: api/revenue/chart
        [HttpGet("chart")]
        public async Task<IActionResult> GetChartData([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<RevenueChartDto>? chartData = await _revenueRepo.GetChartDataAsync(query);

            return Ok(chartData);
        }

        // GET: api/revenue/invoices
        [HttpGet("invoices")]
        public async Task<IActionResult> GetLatestInvoices([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<LatestInvoiceDto>? latestInvoices = await _revenueRepo.GetLatestInvoicesAsync();

            return Ok(latestInvoices);
        }
    }
}