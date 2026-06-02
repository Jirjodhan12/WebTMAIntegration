namespace WebTMAIntegration.Models.Entities
{
    public class FloorEntity
    {
        public int FloorId { get; set; }
        public int? BuildingId { get; set; }
        public int? FloorTypeId { get; set; }
        public string? FloorCode { get; set; }
        public string? BuildingCode { get; set; }
        public string? Alias { get; set; }
        public string? FloorName { get; set; }
        public int? Sequence { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
  
    }
}
