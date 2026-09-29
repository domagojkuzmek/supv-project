using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supv.Src.Supv.Contracts;

namespace Supv.Src.Supv.Api;

[ApiVersion(1.0)]
[ApiController]
[Route("/api/v{version:apiVersion}/vehicles")]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetVehicles(
        [FromServices] AppDbContext db,
        [FromServices] IMapper mapper)
    {
        var vehicles = await db.Vehicles.AsNoTracking().ToListAsync();

        var vehiclesDto = mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        return Ok(vehiclesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(
        Guid id,
        [FromServices] AppDbContext db,
        [FromServices] IMapper mapper)
    {
        var vehicle = await db.Vehicles.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        if (vehicle == null)
        {
            return NotFound();
        }

        var vehicleDto = mapper.Map<VehicleDTO>(vehicle);

        return Ok(vehicleDto);
    }
}
