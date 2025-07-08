using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccessManagement.Domain.Enums;

namespace UserAccessManagement.Domain.Entities
{
    public class AccessGrant
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; } = default!;

        public AccessLevel AccessLevel { get; set; }
    }
}
