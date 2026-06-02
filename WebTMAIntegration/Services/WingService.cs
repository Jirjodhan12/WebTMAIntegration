using WebTMAIntegration.Client;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Services.Interfaces;

namespace WebTMAIntegration.Services
{
    public class WingService : IWingService
    {
        private readonly UIHealthCareClient _uiHealthCare;

        public WingService(UIHealthCareClient uiHealthCare)
        {
            _uiHealthCare = uiHealthCare;
        }

        public Task<PagedResponseModel<WingDto>> GetWingAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null)
        {
            var url = $"Areas?pageIndex={pageIndex}&pageSize={pageSize}";

            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }

            return _uiHealthCare.GetAsync<PagedResponseModel<WingDto>>(url);
        }
    }
}
