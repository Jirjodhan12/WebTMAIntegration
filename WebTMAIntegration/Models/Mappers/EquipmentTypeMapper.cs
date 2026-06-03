using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Models.Mappers
{
    public static class EquipmentTypeMapper
    {
        public static EquipmentTypeEntity ToEntity(EquipmentTypeDto equipmentTypeDto)
        {
            return new EquipmentTypeEntity
            {
                Id = equipmentTypeDto.Id,
                Code = equipmentTypeDto.Code,
                Description = equipmentTypeDto.Description
            };
        }
    }
}
