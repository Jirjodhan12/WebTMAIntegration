using Microsoft.AspNetCore.Mvc;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Mappers;
using WebTMAIntegration.Services.Interfaces;

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
        public async Task<IActionResult> Index(
            int pageIndex = 0,
            int pageSize = 100)
        {

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
                "modifierId",
                "modifiedDate"
            };
            var floors = await _floorService.GetFloorAsync(pageIndex, pageSize, columns);

            return View(floors);
        }

        public async Task<IActionResult> Sync()
        {
            try
            {
                int pageIndex = 0;
                int pageSize = 100;
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
                    "modifierId",
                    "modifiedDate"
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
