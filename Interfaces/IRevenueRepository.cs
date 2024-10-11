using peace_api.Dtos.Revenue;
using peace_api.Helpers;

namespace peace_api.Interfaces
{
    public interface IRevenueRepository
    {
        public Task<RevenueCardDto> GetCardDataAsync(RevenueQueryObject query);
        public Task<List<RevenueChartDto>> GetChartDataAsync(RevenueQueryObject query);
        public Task<List<LatestInvoiceDto>> GetLatestInvoicesAsync();
    }
}