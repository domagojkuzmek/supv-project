using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supv.Src.Supv.Contracts;

namespace Supv.Src.Supv.Api;

[ApiVersion(1.0)]
[ApiController]
[Route("/api/v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromServices] AppDbContext db, [FromServices] IMapper mapper)
    {
        var users = await db.Users
        .AsNoTracking()
        .ToListAsync();
        var usersDto = mapper.Map<IEnumerable<UserDTO>>(users);
        return Ok(usersDto);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid id, [FromServices] AppDbContext db, [FromServices] IMapper mapper)
    {
        var user = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        var userDto = mapper.Map<UserDTO>(user);

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromServices] IValidator<CreateUserDto> validator,
        [FromServices] AppDbContext db,
        [FromBody] CreateUserDto createUserDto)
    {
        var result = await validator.ValidateAsync(createUserDto);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        var user = new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            PasswordHash = createUserDto.Password,
            RoleType = createUserDto.RoleType,
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(CreateUser),
            new { id = user.Id },
            user);
    }

    /* [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser()
    {
        return NoContent();
    }
    */
}
