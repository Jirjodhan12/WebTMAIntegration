using WebTMAIntegration.Models.Entities;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface IWingRepository
    {
        public Task<int> SaveWingsAsync(List<WingEntity> buildings);
    }
}
