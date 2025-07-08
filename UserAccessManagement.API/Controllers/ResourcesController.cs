using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserAccessManagement.Application.Interfaces;
using UserAccessManagement.Domain.Entities;

namespace UserAccessManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateResource([FromBody] Resource resource)
        {
            var created = await _resourceService.CreateResourceAsync(resource);
            return CreatedAtAction(nameof(GetResourceById), new { id = created.Id }, created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _resourceService.GetAllResourcesAsync();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceById(Guid id)
        {
            var resource = await _resourceService.GetResourceByIdAsync(id);
            if (resource == null) return NotFound();
            return Ok(resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(Guid id)
        {
            var success = await _resourceService.DeleteResourceAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
