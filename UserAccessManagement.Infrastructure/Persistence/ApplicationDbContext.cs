using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccessManagement.Domain.Entities;

namespace UserAccessManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<AccessGrant> AccessGrants => Set<AccessGrant>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccessGrant>()
                .HasOne(a => a.User)
                .WithMany(u => u.AccessGrants)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<AccessGrant>()
                .HasOne(a => a.Resource)
                .WithMany(r => r.AccessGrants)
                .HasForeignKey(a => a.ResourceId);
        }
    }
}
