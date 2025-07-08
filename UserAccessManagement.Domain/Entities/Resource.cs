using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccessManagement.Domain.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ICollection<AccessGrant> AccessGrants { get; set; } = new List<AccessGrant>();
    }
}
