using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Entities;
using URLStatus.Domain.Enums;

namespace URLStatus.Application.Services.ResultRuleCheckers.PropertyCheckers
{
    public class ResponseTimeChecker : PropertyCheckerBase<int>
    {
        public ResponseTimeChecker() : base(ResultPropertyEnum.ResponseTime)
        {
        }

        protected override int ExtractValue(ResultData data)
        {
            return (int)Math.Round(data.ResponseTime.TotalMilliseconds);
        }

        protected override bool IsSatisfied(ResultRule rule, int value)
        {
            if (!int.TryParse(rule.Value, out int ruleResponseTime))
            {
                throw new ArgumentException("Invalid response time", nameof(rule.Value));
            }

            var result = rule.Operator switch
            {
                ResultPropertyCompareOperatorEnum.GreaterThan => value > ruleResponseTime,
                ResultPropertyCompareOperatorEnum.LessThan => value < ruleResponseTime,
                _ => throw new ArgumentException("Invalid operator")
            };

            return result;
        }
    }
}
