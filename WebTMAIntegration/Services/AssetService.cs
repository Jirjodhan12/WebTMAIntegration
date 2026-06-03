using WebTMAIntegration.Client;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Services.Interfaces;

namespace WebTMAIntegration.Services
{
    public class AssetService : IAssetService
    {
        private readonly UIHealthCareClient _uiHealthCare;

        public AssetService(UIHealthCareClient uiHealthCare)
        {
            _uiHealthCare = uiHealthCare;
        }
        public Task<PagedResponseModel<SiteDto>> GetEquipmentsAsync(
            int pageIndex = 0,
            int pageSize = 1000,
            List<string>? columns = null)
        {
            var url = $"Equipment?pageIndex={pageIndex}&pageSize={pageSize}";

            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }

            return _uiHealthCare.GetAsync<PagedResponseModel<SiteDto>>(url);
        }
        public Task<PagedResponseModel<EquipmentTypeDto>> GetEquipmentTypesAsync(
            int pageIndex = 0,
            int pageSize = 1000,
            List<string>? columns = null)
        {
            var url = $"EquipmentTypes?pageIndex={pageIndex}&pageSize={pageSize}";

            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }

            return _uiHealthCare.GetAsync<PagedResponseModel<EquipmentTypeDto>>(url);
        }

        public Task<PagedResponseModel<EquipmentSubTypeDto>> GetEquipmentSubTypesAsync(
            int pageIndex = 0,
            int pageSize = 1000,
            List<string>? columns = null)
        {
            var url = $"EquipmentSubTypes?pageIndex={pageIndex}&pageSize={pageSize}";

            if (columns != null && columns.Any())
            {
                url += $"&columns={string.Join(",", columns)}";
            }

            return _uiHealthCare.GetAsync<PagedResponseModel<EquipmentSubTypeDto>>(url);
        }
    }
}
