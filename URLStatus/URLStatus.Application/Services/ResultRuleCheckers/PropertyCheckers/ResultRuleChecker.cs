using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Services.ResultRuleCheckers.PropertyCheckers
{
    public class ResultRuleChecker
    {
        private readonly ResultData _data;

        private readonly PropertyCheckerFactory _propertyCheckerFactory;

        public ResultRuleChecker(ResultData data)
        {
            _data = data;
            _propertyCheckerFactory = new PropertyCheckerFactory();
        }

        public bool CheckRule(ResultRule rule)
        {
            var propertyChecker = _propertyCheckerFactory.GetPropertyChecker(rule);
            return propertyChecker.Check(rule, _data);
        }

        public bool CheckRuleSet(ResultRuleSet ruleSet)
        {
            if (ruleSet.Operator == Domain.Enums.ResultRuleSetOperatorEnum.Or)
            {
                return ruleSet.Rules.Any(r => CheckRule(r));
            }
            else
            {
                return ruleSet.Rules.All(r => CheckRule(r));
            }
        }
    }
}
