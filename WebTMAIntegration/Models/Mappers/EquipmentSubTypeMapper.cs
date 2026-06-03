using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Models.Mappers
{
    public static class EquipmentSubTypeMapper
    {
        public static EquipmentSubTypeEntity ToEntity(EquipmentSubTypeDto equipmentSubTypeDto)
        {
            return new EquipmentSubTypeEntity
            {
                Id = equipmentSubTypeDto.Id,
                Code = equipmentSubTypeDto.Code,
                Description = equipmentSubTypeDto.Description,
                ParentId = equipmentSubTypeDto.ParentId
            };
        }
    }
}
