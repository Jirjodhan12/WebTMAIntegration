using WebTMAIntegration.Client;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Services.Interfaces;

namespace WebTMAIntegration.Services
{
    public class FloorService : IFloorService
    {
        private readonly UIHealthCareClient _uiHealthCare;

        public FloorService(UIHealthCareClient uiHealthCare)
        {
            _uiHealthCare = uiHealthCare;
        }

        public Task<PagedResponseModel<FloorDto>> GetFloorAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null)
        {
            var url = $"Floors?pageIndex={pageIndex}&pageSize={pageSize}";
            Console.WriteLine(url);
            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }
            Console.WriteLine(url);
            return _uiHealthCare.GetAsync<PagedResponseModel<FloorDto>>(url);
        }
    }
}
