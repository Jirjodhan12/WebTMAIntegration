using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface IFloorRepository
    {
        Task<int> SaveFloorsAsync(List<FloorEntity> buildings);
        Task<List<FloorViewModel>> GetFloorsAsync();
    }
}
