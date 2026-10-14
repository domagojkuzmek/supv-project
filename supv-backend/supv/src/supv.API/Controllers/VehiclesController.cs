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
        [FromServices] IMapper mapper,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
        {
            return BadRequest("page must be >= 1.");
        }

        if (pageSize is < 1 or > 100)
        {
            return BadRequest("pageSize must be between 1 and 100.");
        }

        var query = db.Vehicles.AsNoTracking().OrderByDescending(u => u.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var vehicles = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

        var vehiclesDto = mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        return Ok(new
        {
            Data = vehiclesDto,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
            totalCount / (double)pageSize),
            HasNextPage = page * (long)pageSize < totalCount,
            HasPreviousPage = page > 1,
        });
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

    [HttpPost]
    public async Task<IActionResult> CreateVehicle(
        [FromBody] CreateVehicleDto createVehicleDto,
        [FromServices] IValidator<CreateVehicleDto> validator,
        [FromServices] AppDbContext db)
    {
        var result = await validator.ValidateAsync(createVehicleDto);

        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var vehicle = new Vehicle
        {
            RegistrationNumber = createVehicleDto.RegistrationNumber,
            VehicleCategory = createVehicleDto.VehicleCategory,
            Brand = createVehicleDto.Brand,
            Model = createVehicleDto.Model,
            PurchaseDate = createVehicleDto.PurchaseDate,
            PurchaseType = createVehicleDto.PurchaseType,
            Status = createVehicleDto.Status,
            // CreatedBy = get the logged in user,
        };

        db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(CreateVehicle),
            new { id = vehicle.Id },
            vehicle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceVehicle(
        Guid id,
        [FromBody] ReplaceVehicleDto replaceVehicleDto,
        [FromServices] IValidator<ReplaceVehicleDto> validator,
        [FromServices] AppDbContext db,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(replaceVehicleDto);

        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var vehicle = await db.Vehicles
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (vehicle is null)
        {
            return NotFound();
        }

        vehicle.RegistrationNumber = replaceVehicleDto.RegistrationNumber;
        vehicle.VehicleCategory = replaceVehicleDto.VehicleCategory;
        vehicle.Brand = replaceVehicleDto.Brand;
        vehicle.Model = replaceVehicleDto.Model;
        vehicle.PurchaseDate = replaceVehicleDto.PurchaseDate;
        vehicle.PurchaseType = replaceVehicleDto.PurchaseType;
        vehicle.Status = replaceVehicleDto.Status;
        // CreatedBy = get the logged in user,

        await db.SaveChangesAsync();

        return Ok(vehicle);
    }

    [HttpPatch("{id}")]
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
