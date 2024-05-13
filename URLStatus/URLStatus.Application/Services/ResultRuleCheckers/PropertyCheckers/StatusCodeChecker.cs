using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Entities;
using URLStatus.Domain.Enums;

namespace URLStatus.Application.Services.ResultRuleCheckers.PropertyCheckers
{
    public class StatusCodeChecker : PropertyCheckerBase<int>
    {
        public StatusCodeChecker() : base(ResultPropertyEnum.StatusCode)
        {
        }

        protected override int ExtractValue(ResultData data)
        {
            return data.StatusCode;
        }

        protected override bool IsSatisfied(ResultRule rule, int value)
        {
            if (!int.TryParse(rule.Value, out int ruleCode))
            {
                throw new ArgumentException("Invalid status code", nameof(rule.Value));
            }

            var result = rule.Operator switch
            {
                ResultPropertyCompareOperatorEnum.Equal => value == ruleCode,
                ResultPropertyCompareOperatorEnum.NotEqual => value != ruleCode,
                _ => throw new ArgumentException("Invalid operator")
            };

            return result;
        }
    }
}
