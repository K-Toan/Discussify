using Discussify.IdentityService.Models;
using Discussify.IdentityService.Models.Dtos;
using Discussify.IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.IdentityService.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly UserManager<AppUser> _userManager;

    public AuthenticationController(JwtService jwtService, UserManager<AppUser> userManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);

        // found user
        if (user != null)
        {
            // input password matches
            if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = _jwtService.CreateToken(user);
                return Ok();
            }
            // password doesn't match
            else
            {
                return BadRequest("Password doesn't match");
            }
        }
        // can't found user
        return BadRequest("User doesn't exist");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new AppUser { UserName = registerDto.Username, Email = registerDto.Email };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        await _userManager.AddToRoleAsync(user, AppUserRole.User);

        if (result.Succeeded)
        {
            return Ok("User created successfully!");
        }

        return BadRequest(result.Errors);
    }
}