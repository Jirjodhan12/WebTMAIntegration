namespace WebTMAIntegration.Models.DTOs
{
    public class BuildingDto
    {
        public int Id { get; set; }

        public string? FacilityCode { get; set; }

        public int? facilityId { get; set; }
        public int? DivisionId { get; set; }

        public int BuildingTypeId { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool Active { get; set; }

        public int? RegionId { get; set; }

        public int? CreatorId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
