using Microsoft.AspNetCore.Mvc;
using WebTMAIntegration.Data;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Mappers;
using WebTMAIntegration.Services.Interfaces;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Controllers
{
    public class FloorController : Controller
    {
        public readonly IFloorService _floorService;
        public readonly IFloorRepository _floorRepository;

        public FloorController(IFloorService floorService, IFloorRepository floorRepository)
        {
            _floorService = floorService;
            _floorRepository = floorRepository;
        }
        public async Task<IActionResult> Index()
        {

            try
            {
                var floors = await _floorRepository.GetFloorsAsync();
                return View(floors);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;

                return View(new List<FloorViewModel>());
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
                    "buildingId",
                    "buildingCode",
                    "floorTypeId",
                    "floorUniqueId",
                    "name",
                    "active",
                    "creatorId",
                    "createdDate",
                };

                var floors = await _floorService.GetFloorAsync(pageIndex, pageSize, columns);

                var entities = floors.Data
                    .Select(FloorMapper.ToEntity)
                    .ToList();
                var rowAffected = await _floorRepository.SaveFloorsAsync(entities);

                Console.WriteLine("Row Affected: " + rowAffected);

                TempData["Success"] = $"Floor data synced successfully. {rowAffected}  inserted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
