/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFCore
{
    public class TrafficAggregatedRepository : ITrafficAggregatedRepository
    {
        private readonly ECommerceDataContext _dbContext;

        public TrafficAggregatedRepository(ECommerceDataContext context)
        {
            _dbContext = context;
        }

        private ECommerceDataContext GetContext(ContextSession session)
        {
            _dbContext.Session = session;
            return _dbContext;
        }

        public Task<IEnumerable<AggregatedData>> GetDataByPeriod(DateTime start, DateTime end,
            Expression<Func<Traffic, AggregatedData>> groupDataSelector, ContextSession session)
        {
            return GetData(
                obj => obj.Date >= start && obj.Date <= end,
                groupDataSelector,
                group => new AggregatedData {Group = group.Key, Sum = group.Sum(x => x.Sum)},
                session);
        }

        private async Task<IEnumerable<AggregatedData>> GetData(
            Expression<Func<Traffic, bool>> filter,
            Expression<Func<Traffic, AggregatedData>> groupDataSelector,
            Expression<Func<IGrouping<int, AggregatedData>, AggregatedData>> groupExpression,
            ContextSession session)
        {
            var context = GetContext(session);
            return await context.Traffic
                .AsNoTracking()
                .Where(filter)
                .Select(groupDataSelector)
                .GroupBy(obj => obj.Group)
                .Select(groupExpression).ToListAsync();
        }
    }
}