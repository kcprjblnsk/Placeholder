using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using URLStatus.Application.Exceptions;
using URLStatus.Application.Interfaces;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Services
{
    public class CurrentAccountProvider : ICurrentAccountProvider
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthenticationDataProvider _authenticationDataProvider;

        public CurrentAccountProvider(IAuthenticationDataProvider authenticationDataProvider,IApplicationDbContext applicationDbContext)
        {
            _authenticationDataProvider = authenticationDataProvider;
            _applicationDbContext = applicationDbContext;

        }

        public async Task<Account> GetAuthenticatedAccount()
        {
            var accountId = await GetAccountId();
            if (accountId == null)
            {
                throw new UnauthorizedException();
            }

            var account = await _applicationDbContext.Accounts.Cacheable().FirstOrDefaultAsync(a => a.Id == accountId.Value);
            if (account == null)
            {
                throw new ErrorException("AccountDoesNotExist");
            }
            return account;
        }

        public async Task<int?> GetAccountId()
        {
            var userId = _authenticationDataProvider.GetUserId();
            if (userId != null)
            {
                return await _applicationDbContext.AccountUsers
                    .Where(au => au.UserId == userId.Value)
                    .OrderBy(au => au.Id) //needs to be sorted !!!!! possible 2 query 2 different outputs
                    .Select(au => (int?)au.UserId)
                    .Cacheable()
                    .FirstOrDefaultAsync();
            }
            return null;
            
        }

        
    }
}
