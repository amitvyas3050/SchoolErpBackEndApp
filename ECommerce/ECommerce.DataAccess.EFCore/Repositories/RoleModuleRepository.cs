/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.Entities;
using ECommerce.DataAccess.EFCore.Extensions;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFCore
{
    public class RoleModuleRepository : BaseRepository<RoleModule, ECommerceDataContext>, IRoleModuleRepository
    {
        public RoleModuleRepository(ECommerceDataContext context) : base(context)
        {
        }

        //public async RoleMdulesForRole(int roleID)
        //{

        //}

        public override async Task<RoleModule> Get(int id, ContextSession session)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .Include(c => c.Module)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<RoleModule>, int)> GetFilteredListWithTotalCount(RoleModuleGridFilter filter,ContextSession session)
        {
            var query = GetEntities(session).ApplyFilter(filter);

            return (
                await query
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .Include(c => c.Module)
                    .Include(c => c.Role)
                    .ToArrayAsync(),
                await query
                    .CountAsync());
        }

        public async Task<IEnumerable<RoleModule>> GetModulesForRole(int roleID, ContextSession session)
        {
            return await GetEntities(session).Where(x => x.RoleId == roleID).ToListAsync();
        }

       

    }
}