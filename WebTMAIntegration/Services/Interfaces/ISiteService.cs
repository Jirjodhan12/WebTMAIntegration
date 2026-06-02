using Microsoft.Data.SqlClient.DataClassification;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.DTOs;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Services.Interfaces
{
    public interface ISiteService
    {
        Task<PagedResponseModel<SiteDto>> GetSiteAsync(
            int pageIndex = 0,
            int pageSize = 100,
            List<string>? columns = null);
    }
    
}
