namespace WebTMAIntegration.Models.Entities
{
    public class EquipmentSubTypeEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
