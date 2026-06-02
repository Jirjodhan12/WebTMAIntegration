namespace WebTMAIntegration.ViewModels
{
    public class BuildingViewModel
    {
        public int BuildingId { get; set; }

        public string? SiteCode { get; set; }

        public int? CityId { get; set; }

        public string? Address { get; set; }

        public string BuildingName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
