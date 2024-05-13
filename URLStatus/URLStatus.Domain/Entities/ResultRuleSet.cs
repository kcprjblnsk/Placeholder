using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Enums;

namespace URLStatus.Domain.Entities
{
    public class ResultRuleSet
    {
        public List<ResultRule> Rules { get; set; } = new List<ResultRule>();
        public ResultRuleSetOperatorEnum Operator { get; set; } = ResultRuleSetOperatorEnum.Or;
    }
}
