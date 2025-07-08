using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccessManagement.Application.DTOs;

namespace UserAccessManagement.Application.Interfaces
{
    public interface IAccessGrantService
    {
        Task<bool> GrantAccessAsync(GrantAccessDto dto);
        Task<bool> RevokeAccessAsync(Guid userId, Guid resourceId);
        Task<IEnumerable<UserResourceAccessDto>> GetResourcesByUserAsync(Guid userId);
    }
}
