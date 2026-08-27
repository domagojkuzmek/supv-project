using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supv.Src.Supv.Contracts;

[ApiVersion(1.0)]
[ApiController]
[Route("/api/v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromServices] AppDbContext db, [FromServices] IMapper mapper)
    {
        var users = await db.Users.AsNoTracking().ToListAsync();
        var usersDto = mapper.Map<IEnumerable<UserDTO>>(users);
        return Ok(usersDto);
    }
}
