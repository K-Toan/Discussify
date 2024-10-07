using Discussify.IdentityService.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Discussify.IdentityService.Controllers;

[ApiController]
[Route("api/users")]
public class AppUsersController : ControllerBase
{
    private readonly IdentityServiceDbContext _context;

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _context.AppUsers.ToListAsync());
    }
}