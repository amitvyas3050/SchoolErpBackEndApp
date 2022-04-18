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
    public class OrderRepository : BaseRepository<Order, ECommerceDataContext>, IOrderRepository
    {
        public OrderRepository(ECommerceDataContext context) : base(context)
        {
        }

        public override async Task<Order> Get(int id, ContextSession session)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .Include(c => c.Country)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<Order>, int)> GetFilteredListWithTotalCount(OrdersGridFilter filter,
            ContextSession session)
        {
            var query = GetEntities(session).ApplyFilter(filter);

            return (
                await query
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .Include(c => c.Country)
                    .ToArrayAsync(),
                await query
                    .CountAsync());
        }
    }
}