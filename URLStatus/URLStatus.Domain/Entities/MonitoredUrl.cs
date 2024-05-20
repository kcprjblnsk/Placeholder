﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Common;

namespace URLStatus.Domain.Entities
{
    public class MonitoredUrl : DomainEntity
    {
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;
        public int AccountId { get; set; }
        public Account Account { get; set; } = default!;

        public string Url { get; set; } = "";

        public ResultRuleSet RuleSet { get; set; } = new ResultRuleSet();
        public List<Result> Results { get; set; } = new List<Result>();
    }
}
