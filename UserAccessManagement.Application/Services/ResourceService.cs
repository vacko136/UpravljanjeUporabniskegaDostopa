using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccessManagement.Application.Interfaces;
using UserAccessManagement.Domain.Entities;
using UserAccessManagement.Infrastructure.Persistence;

namespace UserAccessManagement.Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _context;

        public ResourceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<Resource?> GetResourceByIdAsync(Guid id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task<Resource> CreateResourceAsync(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<bool> DeleteResourceAsync(Guid id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return false;

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
