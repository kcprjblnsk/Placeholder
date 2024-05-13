using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Enums;

namespace URLStatus.Domain.Entities
{
    public class ResultRule
    {
        public required ResultPropertyEnum Property { get; set; }
        public required string Value { get; set; }
        public required ResultPropertyCompareOperatorEnum Operator { get; set; }
    }
}
