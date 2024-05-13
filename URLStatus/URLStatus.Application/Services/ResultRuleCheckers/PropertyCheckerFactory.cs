using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Application.Services.ResultRuleCheckers.PropertyCheckers;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Services.ResultRuleCheckers
{
    public class PropertyCheckerFactory
    {
        private List<IPropertyChecker> _checkers = new List<IPropertyChecker>()
        {
            new ContentChecker(),
            new ResponseTimeChecker(),
            new StatusCodeChecker(),
        };

        public IPropertyChecker GetPropertyChecker(ResultRule rule)
        {
            foreach (var checker in _checkers)
            {
                if (checker.CanHandle(rule))
                {
                    return checker;
                }
            }

            throw new NotImplementedException("No property checker defined for this type of rule.");
        }
    }
}
