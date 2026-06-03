using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface IAssetRepository
    {
        Task<int> SaveEquipmentTypesAsync(List<EquipmentTypeEntity> equipments);
        Task<int> SaveEquipmentSubTypesAsync(List<EquipmentSubTypeEntity> equipments);
    }
}
