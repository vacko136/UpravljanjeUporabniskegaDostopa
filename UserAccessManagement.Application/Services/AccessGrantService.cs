using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccessManagement.Application.DTOs;
using UserAccessManagement.Application.Interfaces;
using UserAccessManagement.Domain.Entities;
using UserAccessManagement.Domain.Enums;
using UserAccessManagement.Infrastructure.Persistence;

namespace UserAccessManagement.Application.Services
{
    public class AccessGrantService : IAccessGrantService
    {
        private readonly ApplicationDbContext _context;

        public AccessGrantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> GrantAccessAsync(GrantAccessDto dto)
        {
            var existing = await _context.AccessGrants
                .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.ResourceId == dto.ResourceId);

            if (existing != null)
            {
                existing.AccessLevel = Enum.Parse<AccessLevel>(dto.AccessLevel);
            }
            else
            {
                var grant = new AccessGrant
                {
                    UserId = dto.UserId,
                    ResourceId = dto.ResourceId,
                    AccessLevel = Enum.Parse<AccessLevel>(dto.AccessLevel)
                };

                _context.AccessGrants.Add(grant);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RevokeAccessAsync(Guid userId, Guid resourceId)
        {
            var grant = await _context.AccessGrants
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ResourceId == resourceId);

            if (grant == null)
                return false;

            _context.AccessGrants.Remove(grant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserResourceAccessDto>> GetResourcesByUserAsync(Guid userId)
        {
            return await _context.AccessGrants
                .Where(x => x.UserId == userId)
                .Include(x => x.Resource)
                .Select(x => new UserResourceAccessDto
                {
                    ResourceId = x.ResourceId,
                    ResourceName = x.Resource.Name,
                    AccessLevel = x.AccessLevel.ToString()
                })
                .ToListAsync();
        }
    }
}
