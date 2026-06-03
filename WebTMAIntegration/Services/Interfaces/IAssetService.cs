using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;

namespace WebTMAIntegration.Services.Interfaces
{
    public interface IAssetService
    {
        Task<PagedResponseModel<SiteDto>> GetEquipmentsAsync(
            int pageIndex = 0,
            int pageSize = 1000,
            List<string>? columns = null);
        Task<PagedResponseModel<EquipmentTypeDto>> GetEquipmentTypesAsync(
            int pageIndex = 0,
            int pageSize = 1000,
            List<string>? columns = null);

        Task<PagedResponseModel<EquipmentSubTypeDto>> GetEquipmentSubTypesAsync(
            int pageIndex = 0,
            int pageSize = 1000,
            List<string>? columns = null);
    }
}
