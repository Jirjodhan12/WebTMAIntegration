using Microsoft.AspNetCore.Mvc;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Mappers;
using WebTMAIntegration.Services.Interfaces;


namespace WebTMAIntegration.Controllers
{
    public class SiteController : Controller
    {
        public readonly ISiteService _siteService;
        public readonly ISiteRepository _siteRepository;

        public SiteController(ISiteService siteService, ISiteRepository siteRepository)
        {
            _siteService = siteService;
            _siteRepository = siteRepository;
        }
        public async Task<IActionResult> Index()
        {
            var sites = await _siteRepository.GetSitesAsync();

            return View(sites);
        }


        [HttpPost]
        public async Task<IActionResult> Sync()
        {
            try
            {
                int pageIndex = 0;
                int pageSize = 1000;
                var columns = new List<string>
                {
                    "Id",
                    "regionId",
                    "name",
                    "comment",
                    "code",
                    "active",
                    "facilityTypeId",
                    "divisionId",
                    "districtId",
                    "address1",
                    "address2"
                };

                var sites = await _siteService.GetSiteAsync(pageIndex, pageSize, columns);

                var entities = sites.Data
                    .Select(SiteMapper.ToEntity)
                    .ToList();
                var rowAffected = await _siteRepository.SaveSitesAsync(entities);

                Console.WriteLine("Row Inserted: " + rowAffected);

                TempData["Success"] = $"Campus data synced successfully. {rowAffected} inserted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
