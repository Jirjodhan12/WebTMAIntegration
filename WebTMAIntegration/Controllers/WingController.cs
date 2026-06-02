using Microsoft.AspNetCore.Mvc;
using WebTMAIntegration.Data;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Mappers;
using WebTMAIntegration.Services.Interfaces;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Controllers
{
    public class WingController : Controller
    {

        public readonly IWingService _wingService;
        public readonly IWingRepository _wingRepository;

        public WingController(IWingService wingService, IWingRepository wingRepository)
        {
            _wingService = wingService;
            _wingRepository = wingRepository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var wings = await _wingRepository.GetWingsAsync();
                return View(wings);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;

                return View(new List<WingViewModel>());
            }
        }

        public async Task<IActionResult> Sync()
        {
            try
            {
                int pageIndex = 0;
                int pageSize = 1000;
                var columns = new List<string>
                {
                    "id",
                    "floorId",
                    "name",
                    "active",
                    "creatorId",
                    "createdDate",
                    "areaTypeId",
                    "code",
                    "floorCode"
                };
                var wings = await _wingService.GetWingAsync(pageIndex, pageSize, columns);

                var entities = wings.Data
                    .Select(WingMapper.ToEntity)
                    .ToList();
                var rowAffected = await _wingRepository.SaveWingsAsync(entities);

                Console.WriteLine("Row Affected: " + rowAffected);

                TempData["Success"] = $"Room data synced successfully. {rowAffected} inserted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
