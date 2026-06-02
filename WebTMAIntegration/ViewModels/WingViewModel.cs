namespace WebTMAIntegration.ViewModels
{
    public class WingViewModel
    {
        public int WingId { get; set; }
        public int SiteId { get; set; }

        public string? SiteName { get; set; }
        public string? SiteCode { get; set; }
        public string? BuildingName { get; set; }
        public string? BuildingCode { get; set; }

        public string? FloorName { get; set; }
        public string? FloorCode { get; set; }
        public string? WingName { get; set; }
        public string? WingType { get; set; }
        public string? Code { get; set; }

        public int? CreatedBy { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
