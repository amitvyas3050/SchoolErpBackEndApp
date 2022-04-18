/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore
{
    public abstract class BaseUserClaimRepository<TUserClaim, TContext> : IUserClaimRepository<TUserClaim>
        where TUserClaim : BaseUserClaim, new()
        where TContext : CommonDataContext
    {
        private readonly TContext _dbContext;

        public BaseUserClaimRepository(TContext context)
        {
            _dbContext = context;
        }

        protected TContext GetContext(ContextSession session)
        {
            _dbContext.Session = session;
            return _dbContext;
        }

        public async Task<TUserClaim> Add(TUserClaim userClaim, ContextSession session)
        {
            var context = GetContext(session);
            context.Entry(userClaim).State = EntityState.Added;
            await context.SaveChangesAsync();
            return userClaim;
        }

        public async Task<IList<TUserClaim>> EditMany(IList<TUserClaim> userClaims, ContextSession session)
        {
            var context = GetContext(session);

            foreach (var uc in userClaims)
            {
                context.Entry(uc).State = EntityState.Modified;
            }

            await context.SaveChangesAsync();

            return userClaims;
        }

        public async Task Delete(int userId, string claimType, string claimValue, ContextSession session)
        {
            var context = GetContext(session);
            var itemsToDelete = await context.Set<TUserClaim>()
                .Where(cl => cl.UserId == userId && cl.ClaimType == claimType && cl.ClaimValue == claimValue)
                .ToListAsync();
            context.Set<TUserClaim>().RemoveRange(itemsToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<IList<TUserClaim>> GetByUserId(int userId, ContextSession session)
        {
            var context = GetContext(session);
            var list = await context.Set<TUserClaim>()
                .AsNoTracking()
                .Where(obj => obj.UserId == userId)
                .ToListAsync();

            return list.ToList();
        }

        public async Task<IList<TUserClaim>> GetList(int userId, string claimType, string claimValue,
            ContextSession session)
        {
            var context = GetContext(session);
            return await context.Set<TUserClaim>()
                .AsNoTracking()
                .Where(obj => obj.UserId == userId && obj.ClaimType == claimType && obj.ClaimValue == claimValue)
                .ToListAsync();
        }
    }
}