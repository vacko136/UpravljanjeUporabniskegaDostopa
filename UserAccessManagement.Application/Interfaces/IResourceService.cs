using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccessManagement.Domain.Entities;

namespace UserAccessManagement.Application.Interfaces
{
    public interface IResourceService
    {
        Task<Resource> CreateResourceAsync(Resource resource);
        Task<List<Resource>> GetAllResourcesAsync();
        Task<Resource?> GetResourceByIdAsync(Guid id);
        Task<bool> DeleteResourceAsync(Guid id);
    }
}
