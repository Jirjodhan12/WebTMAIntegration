namespace WebTMAIntegration.Models.Entities
{
    public class SiteEntity
    {
        public int SiteId { get; set; }

        public long? RegionsId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public bool? IsInternal { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsActive { get; set; }

        public int? SiteTypeId { get; set; }

        public int? StateId { get; set; }

        public int? CityId { get; set; }

        public string? Address { get; set; }

        public string? SiteAbbreviation { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public DateTime? UpdatedBy { get; set; }

        public bool? IsSyncRequired { get; set; }
    }
}
