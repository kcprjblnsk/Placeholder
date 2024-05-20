using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URLStatus.Application.Exceptions;
using URLStatus.Application.Interfaces;
using URLStatus.Application.Logic.Abstractions;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Logic.MonitoredUrl
{
    public static class GetQuery
    {
        public class Request : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class Result
        {
            public required string Url { get; set; }

            public required ResultRuleSet RuleSet { get; set; }
        }

        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {
            public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var account = await _currentAccountProvider.GetAuthenticatedAccount();

                var model = await _applicationDbContext.MonitoredUrls.FirstOrDefaultAsync(u => u.Id == request.Id && u.AccountId == account.Id);

                if (model == null)
                {
                    throw new UnauthorizedException();
                }

                return new Result()
                {
                    RuleSet = model.RuleSet,
                    Url = model.Url
                };
            }
        }
    }
}
