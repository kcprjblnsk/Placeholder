using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Application.Interfaces;
using URLStatus.Application.Logic.Abstractions;
using URLStatus.Application.Services;
using URLStatus.Domain.Enums;

namespace URLStatus.Application.Logic.ResultRule
{
    public static class SettingsQuery
    {
        public class Request : IRequest<Result>
        {
        }

        public class Result
        {
            public Dictionary<ResultPropertyEnum, List<ResultPropertyCompareOperatorEnum>> PropertiesCompareOperators { get; set; } = new Dictionary<ResultPropertyEnum, List<ResultPropertyCompareOperatorEnum>>();
        }

        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {
            public Handler(ICurrentAccountProvider currentAccountProvider,
                IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var result = new Result();

                foreach (ResultPropertyEnum ev in Enum.GetValues<ResultPropertyEnum>())
                {
                    var describer = new ResultPropertyDescriber(ev);
                    result.PropertiesCompareOperators[ev] = describer.GetCompareOperators();
                }

                return result;
            }
        }
    }
}
