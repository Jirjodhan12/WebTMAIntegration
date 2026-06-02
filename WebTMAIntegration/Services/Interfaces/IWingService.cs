using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;

namespace WebTMAIntegration.Services.Interfaces
{
    public interface IWingService
    {
        Task<PagedResponseModel<WingDto>> GetWingAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null);
    }
}
