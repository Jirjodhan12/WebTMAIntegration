namespace WebTMAIntegration.Models.DTOs
{
    public class WingDto
    {
        public int Id { get; set; }

        public int? FloorId { get; set; }

        public string? Name { get; set; }

        public bool? Active { get; set; }

        public int? CreatorId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? AreaTypeId { get; set; }

        public string? Code { get; set; }

        public string? FloorCode { get; set; }
    }
}
