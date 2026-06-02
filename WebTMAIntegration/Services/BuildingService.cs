using WebTMAIntegration.Client;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Services.Interfaces;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Services
{
    public class BuildingService: IBuildingService
    {
        private readonly UIHealthCareClient _uiHealthCare;

        public BuildingService(UIHealthCareClient uiHealthCare)
        {
            _uiHealthCare = uiHealthCare;
        }

        public Task<PagedResponseModel<BuildingDto>> GetBuildingAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null)
        {
            var url = $"Buildings?pageIndex={pageIndex}&pageSize={pageSize}";

            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }

            return _uiHealthCare.GetAsync<PagedResponseModel<BuildingDto>>(url);
        }
    }
}
