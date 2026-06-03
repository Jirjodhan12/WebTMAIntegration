using Microsoft.AspNetCore.Mvc;
using WebTMAIntegration.Data;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.Models.Mappers;
using WebTMAIntegration.Services;
using WebTMAIntegration.Services.Interfaces;

namespace WebTMAIntegration.Controllers
{
    public class AssetController : Controller
    {
        public readonly IAssetService _assetService;
        public readonly IAssetRepository _assetRepository;

        public AssetController(IAssetService assetService, IAssetRepository assetRepository)
        {
            _assetService = assetService;
            _assetRepository = assetRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Sync()
        {
            try
            {
                int pageIndex = 0;
                int pageSize = 1000;
                var equipmentTypesColumns = new List<string>
                {
                    "id",
                    "code",
                    "description"
                };

                var equipmentSubTypesColumns = new List<string>
                {
                    "id",
                    "code",
                    "description",
                    "parentId"
                };

                var equipmentTypes = await _assetService.GetEquipmentTypesAsync(pageIndex, pageSize, equipmentTypesColumns);

                var entitiesType = equipmentTypes.Data
                    .Select(EquipmentTypeMapper.ToEntity)
                    .ToList();
                var equipmentTypesRow = await _assetRepository.SaveEquipmentTypesAsync(entitiesType);

                Console.WriteLine("Equipment Types  Row Affected: " + equipmentTypesRow);
                
                var equipmentSubTypes = await _assetService.GetEquipmentSubTypesAsync(pageIndex, pageSize, equipmentSubTypesColumns);

                var entities = equipmentSubTypes.Data
                    .Select(EquipmentSubTypeMapper.ToEntity)
                    .ToList();
                var equipmentSubTypesRow = await _assetRepository.SaveEquipmentSubTypesAsync(entities);

                Console.WriteLine("Equipment Sub Types  Row Affected: " + equipmentSubTypesRow);

                TempData["Success"] = $"Equipment Type and Sub Type data synced successfully. {equipmentTypesRow}, {equipmentSubTypesRow} inserted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
