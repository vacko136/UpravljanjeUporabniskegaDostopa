using Microsoft.AspNetCore.Mvc;
using UserAccessManagement.Application.Services;
using UserAccessManagement.Domain.Entities;
using UserAccessManagement.Application.DTOs;
using UserAccessManagement.Application.Interfaces;


namespace UserAccessManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var result = users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createDto)
        {
            var user = new User
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email
            };

            var createdUser = await _userService.CreateUserAsync(user);

            var userDto = new UserDto
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };

            return CreatedAtAction(nameof(GetUsers), new { id = userDto.Id }, userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
