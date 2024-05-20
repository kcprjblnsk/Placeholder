using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Enums;

namespace URLStatus.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ResultPropertyOperatorsAttribute : Attribute
    {
        public List<ResultPropertyCompareOperatorEnum> CompareOperators { get; private set; } = new List<ResultPropertyCompareOperatorEnum>();
        public ResultPropertyOperatorsAttribute(params ResultPropertyCompareOperatorEnum[] compareOperators)
        {
            CompareOperators = compareOperators.ToList();
        }
    }
}
