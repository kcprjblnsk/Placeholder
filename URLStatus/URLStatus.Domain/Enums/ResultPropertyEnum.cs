using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Attributes;

namespace URLStatus.Domain.Enums
{
    public enum ResultPropertyEnum
    {
        [ResultPropertyOperators(ResultPropertyCompareOperatorEnum.Equal,
            ResultPropertyCompareOperatorEnum.NotEqual)]
        StatusCode = 1,

        [ResultPropertyOperators(ResultPropertyCompareOperatorEnum.Equal,
            ResultPropertyCompareOperatorEnum.NotEqual,
            ResultPropertyCompareOperatorEnum.Contains)]
        Content = 2,

        [ResultPropertyOperators(ResultPropertyCompareOperatorEnum.GreaterThan,
            ResultPropertyCompareOperatorEnum.LessThan)]
        ResponseTime = 3,
    }
}
