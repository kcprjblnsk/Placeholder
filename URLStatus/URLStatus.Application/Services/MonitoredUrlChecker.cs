using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Application.Interfaces;
using URLStatus.Application.Services.ResultRuleCheckers.PropertyCheckers;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Services
{
    public class MonitoredUrlChecker
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IUrlRequestor _urlRequestor;
        private readonly ILogger<MonitoredUrlChecker> _logger;

        public MonitoredUrlChecker(IApplicationDbContext applicationDbContext,
            IUrlRequestor urlRequestor,
            ILogger<MonitoredUrlChecker> logger)
        {
            _applicationDbContext = applicationDbContext;
            _urlRequestor = urlRequestor;
            _logger = logger;
        }
        public async Task CheckUrl(int monitoredUrlId, CancellationToken cancellationToken)
        {
            var monitoredUrl = await _applicationDbContext.MonitoredUrls.FirstOrDefaultAsync(u => u.Id == monitoredUrlId);
            if (monitoredUrl != null)
            {
                await CheckUrl(monitoredUrl, cancellationToken);
            }
        }

        public async Task CheckUrl(MonitoredUrl monitoredUrl, CancellationToken cancellationToken)
        {
            var checkResult = false;

            try
            {
                var resultData = await _urlRequestor.GetUrlResultData(monitoredUrl.Url, cancellationToken);

                var resultRuleChecker = new ResultRuleChecker(resultData);
                checkResult = resultRuleChecker.CheckRuleSet(monitoredUrl.RuleSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Check url error!");
            }

            _applicationDbContext.Results.Add(new Result()
            {
                Date = DateTime.Now,
                Success = checkResult,
                MonitoredUrlId = monitoredUrl.Id,
            });

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
}
