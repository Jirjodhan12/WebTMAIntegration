namespace WebTMAIntegration.ViewModels
{
    public class SiteViewModel
    {
        public int SiteId { get; set; }

        public int RegionsId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Code { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int SiteTypeId { get; set; }

        public int? StateId { get; set; }

        public int? CityId { get; set; }

        public string? Address { get; set; }

        public string? SiteAbbreviation { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}