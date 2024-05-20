using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Application.Services;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Validators
{
    public class ResultRuleValidator : AbstractValidator<ResultRule>
    {
        public ResultRuleValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Value).MaximumLength(1000);
            RuleFor(x => x.Operator).Custom((o, context) =>
            {
                ResultPropertyDescriber describer = new ResultPropertyDescriber(context.InstanceToValidate.Property);
                var allowedOperators = describer.GetCompareOperators();
                if (allowedOperators != null && !allowedOperators.Contains(o))
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure()
                    {
                        PropertyName = context.PropertyPath,
                        ErrorCode = "InvalidOperatorForProperty",
                    });
                }
            });
        }
    }
}
