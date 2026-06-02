using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Models.Mappers
{
    public static class WingMapper
    {
        public static WingEntity ToEntity(WingDto dto)
        {
            return new WingEntity
            {
                WingId = dto.Id != 0 ? dto.Id : 1,

                FloorId = dto.FloorId ?? 1,

                WingName = !string.IsNullOrWhiteSpace(dto.Name)
                    ? dto.Name
                    : "Test Wing",

                IsActive = dto.Active ?? false,

                CreatedBy = dto.CreatorId ?? 1,

                WingType = dto.AreaTypeId ?? 1,

                Code = !string.IsNullOrWhiteSpace(dto.Code)
                    ? dto.Code
                    : "TEST-CODE",

                FloorCode = !string.IsNullOrWhiteSpace(dto.FloorCode)
                    ? dto.FloorCode
                    : "TEST-FLOOR",
            };
        }
    }
}
