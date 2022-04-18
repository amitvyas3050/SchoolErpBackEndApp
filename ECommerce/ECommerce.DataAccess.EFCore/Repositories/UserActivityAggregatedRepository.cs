/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using Common.Utils.Extensions;
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
    public class UserActivityAggregatedRepository : IUserActivityAggregatedRepository
    {
        private readonly ECommerceDataContext _dbContext;

        public UserActivityAggregatedRepository(ECommerceDataContext context)
        {
            _dbContext = context;
        }

        private ECommerceDataContext GetContext(ContextSession session)
        {
            _dbContext.Session = session;
            return _dbContext;
        }

        private async Task<List<AggregatedData>> GetDataForTable(
            Expression<Func<UserActivity, bool>> baseFilter,
            Expression<Func<UserActivity, AggregatedData>> groupDataSelector,
            ContextSession session)
        {
            var context = GetContext(session);
            return await context.UserActivities
                .AsNoTracking()
                .Where(baseFilter)
                .Select(groupDataSelector)
                .GroupBy(obj => obj.Group)
                .Select(group => new AggregatedData {Group = group.Key, Sum = 0, Count = group.Count()})
                .ToListAsync();
        }

        public Task<List<AggregatedData>> GetMonthDataForTable(ContextSession session)
        {
            var today = DateTime.Today;
            var startOfMonth = today.StartOfMonth();
            var endOfMonth = today.EndOfMonth();

            return GetDataForTable(
                obj => obj.Date >= startOfMonth && obj.Date <= endOfMonth,
                obj => new AggregatedData {Group = obj.Date.Day, Sum = 0, Count = obj.Id},
                session);
        }

        public Task<List<AggregatedData>> GetWeekDataForTable(ContextSession session,
            DateTime startOfPreviousWeek, DateTime endOfPreviousWeek)
        {
            return GetDataForTable(
                obj => obj.Date >= startOfPreviousWeek && obj.Date <= endOfPreviousWeek,
                obj => new AggregatedData {Group = obj.Date.Day, Sum = 0, Count = obj.Id},
                session);
        }

        public Task<List<AggregatedData>> GetYearlyDataForTable(ContextSession session, DateTime yearsBefore)
        {
            var today = DateTime.Today;
            return GetDataForTable(
                obj => obj.Date >= yearsBefore && obj.Date <= today,
                obj => new AggregatedData {Group = obj.Date.Year, Sum = 0, Count = obj.Id},
                session);
        }
    }
}