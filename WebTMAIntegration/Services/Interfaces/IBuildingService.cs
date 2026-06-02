using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;

namespace WebTMAIntegration.Services.Interfaces
{
    public interface IBuildingService
    {
        Task<PagedResponseModel<BuildingDto>> GetBuildingAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null);
    }
}
