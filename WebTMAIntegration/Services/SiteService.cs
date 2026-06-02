using WebTMAIntegration.Client;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Services.Interfaces;

namespace WebTMAIntegration.Services
{
    public class SiteService : ISiteService
    {
        private readonly UIHealthCareClient _uiHealthCare;

        public SiteService(UIHealthCareClient uiHealthCare)
        {
            _uiHealthCare = uiHealthCare;
        }

        public Task<PagedResponseModel<SiteDto>> GetSiteAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null)
        {
            var url = $"Facilities?pageIndex={pageIndex}&pageSize={pageSize}";

            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }

            return _uiHealthCare.GetAsync<PagedResponseModel<SiteDto>>(url);
        }
    }
}
