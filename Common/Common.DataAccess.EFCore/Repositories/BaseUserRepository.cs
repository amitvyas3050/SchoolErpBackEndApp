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
    public abstract class BaseUserRepository<TUser, TContext> : BaseDeletableRepository<TUser, TContext>, IUserRepository<TUser>
        where TUser : BaseUser, new()
        where TContext : CommonDataContext
    {
        public BaseUserRepository(TContext context) : base(context)
        {
        }

        public override async Task<TUser> Edit(TUser obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session);
            var context = GetContext(session);
            context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;

            if (string.IsNullOrEmpty(obj.Password))
            {
                context.Entry(obj).Property(x => x.Password).IsModified = false;
            }

            await context.SaveChangesAsync();
            return obj;
        }

        public override async Task<TUser> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session, includeDeleted)
                .Where(obj => obj.Id == id)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(u => u.Settings)
                .FirstOrDefaultAsync();
        }

        public async Task<TUser> GetByLogin(string login, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session, includeDeleted)
                .Where(obj => obj.Login == login)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(u => u.Settings)
                .FirstOrDefaultAsync();
        }

        public async Task<TUser> GetByEmail(string email, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session, includeDeleted)
                .Where(obj => obj.Email == email)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(u => u.Settings)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<TUser>, int)> GetFilteredListWithTotalCount(UsersGridFilter filter,
            ContextSession session, bool includeDeleted = false)
        {
            var query = GetEntities(session, includeDeleted).ApplyFilter(filter);
            return (
                await query
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .Include(u => u.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Include(u => u.Settings)
                    .ToArrayAsync(),
                await query
                    .CountAsync());
        }
    }
}