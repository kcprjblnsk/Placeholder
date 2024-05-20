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
using static URLStatus.Application.Logic.MonitoredUrl.ListQuery.Result;

namespace URLStatus.Application.Logic.MonitoredUrl
{
    public static class ListQuery
    {
        public class Request : IRequest<Result>
        {
        }

        public class Result
        {
            public List<MonitoredUrl> Urls { get; set; } = new List<MonitoredUrl>();

            public class MonitoredUrl
            {
                public required int Id { get; set; }

                public required string Url { get; set; }

                public required DateTimeOffset CreateDate { get; set; }

                public List<CheckResult> RecentResults { get; set; } = new List<CheckResult>();
            }

            public class CheckResult
            {
                public required DateTimeOffset Date { get; set; }

                public required bool Success { get; set; }
            }
        }

        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {
            public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var account = await _currentAccountProvider.GetAuthenticatedAccount();

                var data = await _applicationDbContext.MonitoredUrls.Where(u => u.AccountId == account.Id)
                    .OrderByDescending(u => u.CreateDate)
                    .Select(u => new Result.MonitoredUrl()
                    {
                        Id = u.Id,
                        Url = u.Url,
                        CreateDate = u.CreateDate,
                        RecentResults = u.Results.OrderByDescending(r => r.Date)
                            .Take(10)
                            .Select(r => new CheckResult()
                            {
                                Date = r.Date,
                                Success = r.Success,
                            })
                            .ToList(),
                    })
                    .ToListAsync();

                return new Result()
                {
                    Urls = data
                };
            }
        }
    }
}

