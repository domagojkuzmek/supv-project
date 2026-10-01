using Asp.Versioning;
using AutoMapper;
using FluentValidation;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(
        Guid id,
        [FromServices] AppDbContext db,
        [FromServices] IValidator<UpdateVehicleDto> validator,
        [FromBody] UpdateVehicleDto updateVehicleDto,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(updateVehicleDto, cancellationToken);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        var vehicle = await db.Vehicles
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (vehicle is null)
        {
            return NotFound();
        }

        if (updateVehicleDto.RegistrationNumber is not null)
        {
            vehicle.RegistrationNumber = updateVehicleDto.RegistrationNumber;
        }

        if (updateVehicleDto.VehicleCategory is not null)
        {
            vehicle.VehicleCategory = updateVehicleDto.VehicleCategory.Value;
        }

        if (updateVehicleDto.Brand is not null)
        {
            vehicle.Brand = updateVehicleDto.Brand;
        }

        if (updateVehicleDto.Model is not null)
        {
            vehicle.Model = updateVehicleDto.Model;
        }

        if (updateVehicleDto.PurchaseDate is not null)
        {
            vehicle.PurchaseDate = updateVehicleDto.PurchaseDate.Value;
        }

        if (updateVehicleDto.VehicleCategory.HasValue)
        {
            vehicle.PurchaseType = updateVehicleDto.PurchaseType.Value;
        }

        if (updateVehicleDto.Status is not null)
        {
            vehicle.Status = updateVehicleDto.Status.Value;
        }

        await db.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}
