namespace WebTMAIntegration.ViewModels
{
    public class BuildingViewModel
    {
        public int BuildingId { get; set; }

        public string? SiteName { get; set; }

        public string? SiteCodeValue { get; set; }

        public string? BuildingName { get; set; } = string.Empty;

        public string? BuildingCode { get; set; } = string.Empty;

        public int? BuildingTypeId { get; set; }

        public int? CreatedBy { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
