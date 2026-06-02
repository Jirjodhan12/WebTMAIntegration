using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Mappers
{
    public static class FloorMapper
    {
        public static FloorEntity ToEntity(FloorDto dto)
        {
            return new FloorEntity
            {
                FloorId = dto.Id ?? 1,

                BuildingId = dto.BuildingId ?? 1,

                FloorTypeId = dto.FloorTypeId ?? 1,

                FloorCode = string.IsNullOrWhiteSpace(dto.FloorUniqueId)
                    ? "test"
                    : dto.FloorUniqueId,

                BuildingCode = string.IsNullOrWhiteSpace(dto.BuildingCode)
                    ? "test"
                    : dto.BuildingCode,

                Alias = "test",

                FloorName = string.IsNullOrWhiteSpace(dto.Name)
                    ? "test"
                    : dto.Name,

                Sequence = 1,

                IsActive = dto.Active ?? false,

                CreatedBy = dto.CreatorId ?? 1,

                CreateDate = dto.CreatedDate ?? DateTime.Now,

                UpdatedBy = dto.ModifierId ?? 1,

                UpdatedDate = dto.ModifiedDate ?? DateTime.Now,

                IsSyncRequired = false,

                IsBuildingFloor = false
            };
        }
    }
}