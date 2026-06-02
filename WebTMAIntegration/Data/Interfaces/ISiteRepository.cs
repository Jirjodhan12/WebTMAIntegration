using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data.Interfaces
{
    public interface ISiteRepository
    {
        public Task<int> SaveSitesAsync(List<SiteEntity> buildings);
        public Task<List<SiteViewModel>> GetSitesAsync();
    }
}
