namespace WebTMAIntegration.ViewModels
{
    public class FloorViewModel
    {
        public int FloorId { get; set; }

        public string? SiteName { get; set; }

        public string? SiteCode { get; set; }

        public string? BuildingName { get; set; }

        public string? BuildingCode { get; set; } 
        public string? FloorCode { get; set; }
        public string? FloorName { get; set; }

        public int? CreatedBy { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
