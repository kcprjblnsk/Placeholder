using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Common;

namespace URLStatus.Domain.Entities
{
    public class User : DomainEntity
    {
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public DateTimeOffset RegisterDate { get; set; }

        public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();

    }
}
