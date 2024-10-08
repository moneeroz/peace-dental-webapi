using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using peace_api.Helpers;
using peace_api.Interfaces;

namespace peace_api.Controllers
{
    [Route("api/revenue")]
    [ApiController]
    public class RevenueController(IRevenueRepository revenueRepo) : ControllerBase
    {
        private readonly IRevenueRepository _revenueRepo = revenueRepo;

        // GET: api/revenue/card
        [HttpGet("card")]
        public async Task<IActionResult> GetCardData([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cardData = await _revenueRepo.GetCardDataAsync(query);

            return Ok(cardData);
        }

        // Get: api/revenue/chart
        [HttpGet("chart")]
        public async Task<IActionResult> GetChartData([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chartData = await _revenueRepo.GetChartDataAsync(query);

            return Ok(chartData);
        }

        // GET: api/revenue/invoices
        [HttpGet("invoices")]
        public async Task<IActionResult> GetLatestInvoices([FromQuery] RevenueQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var latestInvoices = await _revenueRepo.GetLatestInvoicesAsync();

            return Ok(latestInvoices);
        }
    }
}