using Microsoft.AspNetCore.Mvc;
using WebTMAIntegration.Data;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.Mappers;
using WebTMAIntegration.Services.Interfaces;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Controllers
{
    public class BuildingController : Controller
    {
        public readonly IBuildingService _buildingService;
        public readonly IBuildingRepository _buildingRepository;

        public BuildingController(IBuildingService buildingService, IBuildingRepository buildingRepository)
        {
            _buildingService = buildingService;
            _buildingRepository = buildingRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var buildings = await _buildingRepository.GetBuildingsAsync();
                return View(buildings);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;

                return View(new List<BuildingViewModel>());
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
                    "facilityCode",
                    "facilityId",
                    "divisionId",
                    "buildingTypeId",
                    "address1",
                    "address2",
                    "code",
                    "name",
                    "active",
                    "regionId",
                    "creatorId",
                    "createdDate",
                };

                var buildings = await _buildingService.GetBuildingAsync(pageIndex, pageSize, columns);

                var entities = buildings.Data
                    .Select(BuildingMapper.ToEntity)
                    .ToList();
                var rowAffected = await _buildingRepository.SaveBuildingsAsync(entities);

                Console.WriteLine("Row Affected: " + rowAffected);

                TempData["Success"] = $"Building data synced successfully. {rowAffected} inserted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}