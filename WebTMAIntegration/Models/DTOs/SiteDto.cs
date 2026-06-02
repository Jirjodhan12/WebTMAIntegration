namespace WebTMAIntegration.Models.DTOs
{
    public class SiteDto
    {
        public int Id { get; set; }
        public int? RegionId { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public string? Code { get; set; }
        public bool Active { get; set; }
        public int? FacilityTypeId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
    }
}
