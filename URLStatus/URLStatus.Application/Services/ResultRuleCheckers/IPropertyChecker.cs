using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Services.ResultRuleCheckers
{
    public interface IPropertyChecker
    {
        bool CanHandle(ResultRule rule);
        bool Check(ResultRule rule,ResultData data);
    }
}
