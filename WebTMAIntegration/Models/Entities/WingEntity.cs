namespace WebTMAIntegration.Models.Entities
{
    public class WingEntity
    {
        public int WingId { get; set; }
        public int? FloorId { get; set; }
        public string WingName { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? WingType { get; set; }
        public string? Code { get; set; }
        public string? FloorCode { get; set; }
    }
}
