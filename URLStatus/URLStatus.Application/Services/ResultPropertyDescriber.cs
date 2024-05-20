using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Attributes;
using URLStatus.Domain.Enums;

namespace URLStatus.Application.Services
{
    public class ResultPropertyDescriber
    {
        private readonly ResultPropertyEnum _property;

        public ResultPropertyDescriber(ResultPropertyEnum property)
        {
            _property = property;
        }

        public List<ResultPropertyCompareOperatorEnum> GetCompareOperators()
        {
            FieldInfo? fieldInfo = typeof(ResultPropertyEnum).GetField(_property.ToString());
            if (fieldInfo != null)
            {
                ResultPropertyOperatorsAttribute? attribute = fieldInfo.GetCustomAttribute<ResultPropertyOperatorsAttribute>();
                if (attribute != null)
                {
                    return attribute.CompareOperators;
                }
            }

            return new List<ResultPropertyCompareOperatorEnum>();
        }
    }
}
