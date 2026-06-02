namespace WebTMAIntegration.Models.DTOs
{
    public class FloorDto
    {
        public int? Id { get; set; }

        public int? BuildingId { get; set; }
        public string? BuildingCode { get; set; }

        public int? FloorTypeId { get; set; }

        public string? FloorUniqueId { get; set; }

        public string? Name { get; set; }

        public bool? Active { get; set; }

        public int? CreatorId { get; set; }

    }
}