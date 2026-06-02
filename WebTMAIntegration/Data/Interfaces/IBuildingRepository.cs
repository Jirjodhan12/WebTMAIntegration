using WebTMAIntegration.Models;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface IBuildingRepository
    {
        //public Task<int> SaveBuildingAsync(BuildingEntity building);
        public Task<int> SaveBuildingsAsync(List<BuildingEntity> buildings);
        public Task<List<BuildingViewModel>> GetBuildingsAsync();
    }
} 
