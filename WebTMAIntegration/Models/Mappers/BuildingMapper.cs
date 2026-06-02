using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Models.Mappers
{
    public static class BuildingMapper
    {
        public static BuildingEntity ToEntity(BuildingDto dto)
        {
            return new BuildingEntity
            {
                // DTO Mapping
                BuildingId = dto.Id,

                SiteCode = dto.FacilityCode ?? "test",

                CityId = dto.DivisionId ?? 1,

                BuildingTypeId = dto.BuildingTypeId,

                Address = $"{dto.Address1 ?? "test"} {dto.Address2 ?? "test"}".Trim(),

                BuildingCode = dto.Code ?? "test",

                BuildingName = dto.Name ?? "test",

                IsActive = dto.Active,

                RegionId = dto.RegionId ?? 1,

                CreatedBy = dto.CreatorId,

                CreateDate = dto.CreatedDate ?? DateTime.UtcNow,

                UpdatedBy = dto.ModifierId ?? 1,

                UpdatedDate = dto.ModifiedDate,

                // Not Available In DTO
                TMSReference = "test",

                ShiftIds = "test",

                IsSyncRequired = false,

                IsFireDrillApplicable = false,

                FireDrillBuildingStartDate = DateTime.UtcNow
            };
        }
    }
}
