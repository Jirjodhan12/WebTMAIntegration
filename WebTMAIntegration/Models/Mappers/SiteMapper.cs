using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Models.Mappers
{
    public static class SiteMapper
    {
        public static SiteEntity ToEntity(SiteDto? dto)
        {
            return new SiteEntity
            {
                SiteId = dto?.Id ?? 1,

                RegionsId = dto?.RegionId ?? 1,

                Name = !string.IsNullOrWhiteSpace(dto?.Name)
                    ? dto.Name
                    : "Test Name",

                Description = !string.IsNullOrWhiteSpace(dto?.Comment)
                    ? dto.Comment
                    : "Test Description",

                Code = !string.IsNullOrWhiteSpace(dto?.Code)
                    ? dto.Code
                    : "TEST",

                IsActive = dto?.Active ?? false,

                SiteTypeId = dto?.FacilityTypeId ?? 1,

                StateId = dto?.DivisionId ?? 1,

                CityId = dto?.DistrictId ?? 1,

                Address = !string.IsNullOrWhiteSpace(dto?.Address1) ||
                          !string.IsNullOrWhiteSpace(dto?.Address2)
                    ? $"{dto?.Address1} {dto?.Address2}".Trim()
                    : "Test Address",

                SiteAbbreviation = !string.IsNullOrWhiteSpace(dto?.Code)
                    ? dto.Code
                    : "TEST",
                CreatedBy = 1
            };
        }
    }
}
