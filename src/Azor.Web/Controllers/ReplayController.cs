using Azor.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Azor.Web.Controllers;

// Controllers/ReplayController.cs
public class ReplayController : Controller
{
    private readonly IReplayImportService _importService;
    private readonly IBattleReplayStorage _storage;

    public ReplayController(IReplayImportService importService, IBattleReplayStorage storage)
    {
        _importService = importService;
        _storage = storage;
    }

    // GET: Replay/Upload
    public IActionResult Upload() => View();

    // POST: Replay/Upload
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile jsonFile, string division, string spawn)
    {
        if (jsonFile == null || jsonFile.Length == 0)
        {
            ModelState.AddModelError("", "Por favor, selecciona un archivo JSON.");
            return View();
        }

        try
        {
            using var stream = jsonFile.OpenReadStream();
            var replay = await _importService.ImportFromStreamAsync(stream);

            replay.Division = division?.Trim();
            replay.Spawn = spawn?.Trim();
        
            _storage.Add(replay);
            TempData["Message"] = $"Partida importada correctamente. Mapa: {replay.Metadata?.Map}";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error al procesar el archivo: {ex.Message}");
            return View();
        }
    }

    // GET: Replay/Index
    public IActionResult Index()
    {
        var replays = _storage.GetAll();
        return View(replays);
    }

    // GET: Replay/Details/5
    public IActionResult Details(int id)
    {
        var replay = _storage.GetById(id);
        if (replay == null) return NotFound();
        return View(replay);
    }

    // GET: Replay/VehicleDetails/5
    public IActionResult VehicleDetails(int vehicleId)
    {
        // Buscar el vehículo en todas las partidas (ineficiente pero es demo)
        var vehicle = _storage.GetAll()
            .SelectMany(r => r.Vehicles)
            .FirstOrDefault(v => v.Id == vehicleId);
        if (vehicle == null) return NotFound();
        return View(vehicle);
    }
}