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
    public abstract class BaseIdentityUserRepository<TUser, TContext> : BaseDeletableRepository<TUser, TContext>,
        IIdentityUserRepository<TUser>
        where TUser : BaseUser, new()
        where TContext : CommonDataContext
    {
        protected BaseIdentityUserRepository(TContext context) : base(context)
        {
        }

        public override async Task<TUser> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .Include(u => u.Claims)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<TUser> GetByLogin(string login, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Where(obj => obj.Login == login)
                .Include(u => u.Claims)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<TUser> GetByEmail(string email, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(u => u.Claims)
                .Where(obj => obj.Email == email)
                .FirstOrDefaultAsync();
        }

        public Task<TUser> GetById(int id, ContextSession session, bool includeDeleted = false)
        {
            return Get(id, session);
        }

        public async Task<IList<TUser>> GetUsersByRole(int roleId, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Include(u => u.Claims)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.UserRoles.Any(ur => ur.RoleId == roleId))
                .ToArrayAsync();
        }

        public async Task<IList<TUser>> GetUsersByClaim(string claimType, string claimValue, ContextSession session,
            bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Include(u => u.Claims)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.Claims.Any(cl => cl.ClaimType == claimType && cl.ClaimValue == claimValue))
                .ToArrayAsync();
        }
    }
}