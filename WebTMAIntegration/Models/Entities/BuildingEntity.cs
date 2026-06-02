
namespace WebTMAIntegration.Models.Entities
{
    public class BuildingEntity
    {
        public int BuildingId { get; set; } 

        public string? SiteCode { get; set; }

        public int? SiteId { get; set; }
        public int? CityId { get; set; }

        public int BuildingTypeId { get; set; }

        public string? Address { get; set; }

        public string BuildingCode { get; set; } = string.Empty;

        public string BuildingName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int? RegionId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }


    }
}
