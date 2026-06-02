
namespace WebTMAIntegration.Models.Entities
{
    public class BuildingEntity
    {
        public int BuildingId { get; set; } 

        public string? SiteCode { get; set; }

        public int? CityId { get; set; }

        public int BuildingTypeId { get; set; }

        public string? Address { get; set; }

        public string BuildingCode { get; set; } = string.Empty;

        public string BuildingName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public string? TMSReference { get; set; }

        public string? ShiftIds { get; set; }

        public int? RegionId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsSyncRequired { get; set; }

        public bool? IsFireDrillApplicable { get; set; }

        public DateTime? FireDrillBuildingStartDate { get; set; }
    }
}
