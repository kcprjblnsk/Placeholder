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
    public static class ExtensionClass
    {
        public static IRuleBuilderOptions<T, string> MustBeUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, prop, context) => {
                    return prop.StartsWith("http://") || prop.StartsWith("https://");
                })
                .WithErrorCode("UrlValidator");
        }
    }
}
