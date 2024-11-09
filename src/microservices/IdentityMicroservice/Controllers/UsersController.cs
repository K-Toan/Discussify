using IdentityMicroservice.Models;
using IdentityMicroservice.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMicroservice.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int pageIndex = 1, int pageSize = 100, string orderBy = "createdat", string keyword = "")
        {
            var usersQuery = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(keyword) || u.Email.Contains(keyword));
            }

            usersQuery = orderBy.ToLower() switch
            {
                "createdat" => usersQuery.OrderBy(u => u.CreatedAt),
                "username" => usersQuery.OrderBy(u => u.UserName),
                "email" => usersQuery.OrderBy(u => u.Email),
                _ => usersQuery.OrderBy(u => u.CreatedAt)
            };

            var totalUsers = await usersQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            var users = await usersQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,
                    UpdateAt = u.UpdatedAt,
                    DeleteAt = u.DeletedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdateAt = user.UpdatedAt,
                DeleteAt = user.DeletedAt
            });
        }
    }
}
