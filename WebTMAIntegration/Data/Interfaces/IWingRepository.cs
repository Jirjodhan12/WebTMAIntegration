using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface IWingRepository
    {
        Task<int> SaveWingsAsync(List<WingEntity> buildings);
        Task<List<WingViewModel>> GetWingsAsync();
    }
}
