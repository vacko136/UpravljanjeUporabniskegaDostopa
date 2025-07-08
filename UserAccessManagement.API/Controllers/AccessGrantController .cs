using Microsoft.AspNetCore.Mvc;
using UserAccessManagement.Application.DTOs;
using UserAccessManagement.Application.Interfaces;

namespace UserAccessManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessGrantController : ControllerBase
    {
        private readonly IAccessGrantService _service;

        public AccessGrantController(IAccessGrantService service)
        {
            _service = service;
        }

        [HttpPost("grant")]
        public async Task<IActionResult> GrantAccess([FromBody] GrantAccessDto dto)
        {
            var result = await _service.GrantAccessAsync(dto);
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("revoke")]
        public async Task<IActionResult> RevokeAccess(Guid userId, Guid resourceId)
        {
            var result = await _service.RevokeAccessAsync(userId, resourceId);
            return result ? Ok() : NotFound();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetResourcesByUser(Guid userId)
        {
            var resources = await _service.GetResourcesByUserAsync(userId);
            return Ok(resources);
        }
    }
}
