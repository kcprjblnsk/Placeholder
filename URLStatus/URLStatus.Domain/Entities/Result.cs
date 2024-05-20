using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Common;

namespace URLStatus.Domain.Entities
{
    public class Result : DomainEntity
    {
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        public int MonitoredUrlId { get; set; }
        public MonitoredUrl Url { get; set; }
        public bool Success { get; set; } = false;
    }
}
