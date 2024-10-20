using AutoMapper;
using Discussify.IdentityService.Data;
using Discussify.IdentityService.Interfaces;
using Discussify.IdentityService.Models;
using Discussify.IdentityService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Discussify.IdentityService.Controllers;

[ApiController]
[Route("api/users")]
public class AppUserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppUserRepository _appUserRepository;

    public AppUserController(IMapper mapper, IAppUserRepository appUserRepository)
    {
        _mapper = mapper;
        _appUserRepository = appUserRepository;
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _appUserRepository.GetAllAsync();
        var userDtos = _mapper.Map<IEnumerable<AppUserDto>>(users);
        return Ok(userDtos);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUserDto>> GetById(int id)
    {
        var user = await _appUserRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        var userDto = _mapper.Map<AppUserDto>(user);
        return Ok(userDto);
    }

    // POST: api/users
    // [HttpPost]
    // public async Task<ActionResult<AppUserDto>> Create(AppUserCreateDto createUserDto)
    // {
    //     var user = _mapper.Map<AppUser>(createUserDto);
    //     var newUser = await _appUserRepository.AddAsync(user);

    //     var userDto = _mapper.Map<AppUserDto>(newUser);
    //     return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
    // }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AppUserUpdateDto updateUserDto)
    {
        var user = await _appUserRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        _mapper.Map(updateUserDto, user);
        await _appUserRepository.UpdateAsync(user);

        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _appUserRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}