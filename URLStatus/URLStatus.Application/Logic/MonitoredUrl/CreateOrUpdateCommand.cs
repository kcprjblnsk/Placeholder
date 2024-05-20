using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using URLStatus.Application.Exceptions;
using URLStatus.Application.Interfaces;
using URLStatus.Application.Logic.Abstractions;
using URLStatus.Application.Validators;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Logic.MonitoredUrl
{
    public static class CreateOrUpdateCommand
    {
        public class Request : IRequest<Result>
        {
            public int? Id { get; set; }

            public required string Url { get; set; }

            public required ResultRuleSet RuleSet { get; set; }

        }
        public class Result
        {
            public required int Id { get; set; }

        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            
            public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
            {
                
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var account = await _currentAccountProvider.GetAuthenticatedAccount();

                Domain.Entities.MonitoredUrl? model = null;
                if (request.Id.HasValue)
                {
                    model = await _applicationDbContext.MonitoredUrls.FirstOrDefaultAsync(u => u.Id == request.Id && u.AccountId == account.Id);
                }
                else
                {
                    model = new Domain.Entities.MonitoredUrl()
                    {
                        AccountId = account.Id
                    };

                    _applicationDbContext.MonitoredUrls.Add(model);
                }

                if (model == null)
                {
                    throw new UnauthorizedException();
                }

                model.Url = request.Url;
                model.RuleSet = request.RuleSet;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new Result()
                {
                    Id = model.Id
                };
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Url).NotEmpty();
                RuleFor(x => x.Url).MaximumLength(400);
                RuleFor(x => x.Url).MustBeUrl();

                RuleForEach(x => x.RuleSet.Rules).SetValidator(new ResultRuleValidator());
            }
        }



    }
}
