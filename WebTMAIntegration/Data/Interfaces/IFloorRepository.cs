using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface IFloorRepository
    {
        public Task<int> SaveFloorsAsync(List<FloorEntity> buildings);
    }
}
