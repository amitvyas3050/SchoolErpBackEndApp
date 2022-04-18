/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using Common.Utils.Extensions;
using ECommerce.Entities;
using ECommerce.Entities.Statistics;
using ECommerce.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace ECommerce.DataAccess.EFCore
{
    public class OrderAggregatedRepository : IOrderAggregatedRepository
    {
        private readonly ECommerceDataContext _dbContext;

        public OrderAggregatedRepository(ECommerceDataContext context)
        {
            _dbContext = context;
        }

        protected ECommerceDataContext GetContext(ContextSession session)
        {
            _dbContext.Session = session;
            return _dbContext;
        }

        public Task<Dictionary<int, List<AggregatedData>>> GetCompletedProfitForYearChartData(IEnumerable<int> statuses,
            ContextSession session)
        {
            var startDate = DateTime.Today.YearBefore();
            return GetChartData(statuses,
                obj => obj.Date >= startDate,
                obj => new AggregatedData {Group = obj.Date.Month, Sum = obj.Value},
                group => new AggregatedData {Group = group.Key, Sum = group.Sum(x => x.Sum)},
                session);
        }

        public async Task<IEnumerable<AggregatedData<DateTime>>> GetProfitForDateRangeChartData(ContextSession session,
            DateTime start, DateTime end)
        {
            var context = GetContext(session);
            return await context.Orders
                .AsNoTracking()
                .Where(obj => obj.Date >= start && obj.Date <= end)
                .GroupBy(obj => obj.Date)
                .OrderBy(obj => obj.Key)
                .Select(group => new AggregatedData<DateTime> {Group = group.Key, Sum = group.Sum(x => x.Value)})
                .ToListAsync();
        }

        public async Task<OrdersProfit> GetProfitInfo(ContextSession session)
        {
            var context = GetContext(session);

            var query = context.Orders.AsNoTracking().AsQueryable();

            var startOfWeekBefore = DateTime.Today.WeekBefore().StartOfWeek();
            var endOfWeekBefore = DateTime.Today.WeekBefore().EndOfWeek();
            var startOfWeekCurrent = DateTime.Today.StartOfWeek();

            var lastWeekCount = await query.Where(x => x.Date >= startOfWeekBefore && x.Date <= endOfWeekBefore)
                .CountAsync();
            var currentWeekCount =
                await query.Where(x => x.Date >= startOfWeekCurrent && x.Date <= DateTime.Now).CountAsync();

            var lastWeekProfit =
                (await query.Where(x => x.Date >= startOfWeekBefore && x.Date <= endOfWeekBefore).Select(x => x.Value)
                    .ToListAsync()).DefaultIfEmpty(0).Sum(x => x);
            var currentWeekProfit =
                (await query.Where(x => x.Date >= startOfWeekCurrent && x.Date <= DateTime.Now).Select(x => x.Value)
                    .ToListAsync()).DefaultIfEmpty(0).Sum(x => x);

            return new OrdersProfit
            {
                LastWeekCount = lastWeekCount,
                CurrentWeekCount = currentWeekCount,
                LastWeekProfit = (int) lastWeekProfit,
                CurrentWeekProfit = (int) currentWeekProfit
            };
        }

        public async Task<OrdersSummary> GetOrdersSummaryInfo(ContextSession session)
        {
            var context = GetContext(session);

            var query = context.Orders.AsNoTracking().AsQueryable();

            var totalCount = query.DeferredCount().FutureValue();

            var startOfMonth = DateTime.Today.AddMonths(-1).StartOfMonth();
            var endOfMonth = DateTime.Today.AddMonths(-1).EndOfMonth();

            var lastMonthCount = query
                .DeferredCount(x => x.Date >= startOfMonth && x.Date <= endOfMonth)
                .FutureValue();

            var startOfWeek = DateTime.Today.AddDays(-7).StartOfWeek();
            var endOfWeek = DateTime.Today.AddDays(-7).EndOfWeek();

            var lastWeekCount = query
                .DeferredCount(x => x.Date >= startOfWeek && x.Date <= endOfWeek).FutureValue();

            var todayCount = await query
                .DeferredCount(x => x.Date >= DateTime.Today && x.Date < DateTime.Today.AddDays(1)).FutureValue()
                .ValueAsync();

            return new OrdersSummary
            {
                Today = todayCount,
                LastMonth = lastMonthCount,
                LastWeek = lastWeekCount,
                Marketplace = totalCount
            };
        }

        public async Task<IEnumerable<OrderTypeStatistic>> GetDataGroupedByCountry(string code, ContextSession session)
        {
            var context = GetContext(session);
            return (await context.Orders.Include(x => x.Country)
                    .AsNoTracking()
                    .Where(x => x.Country.Code == code)
                    .ToListAsync())
                .GroupBy(x => x.Type, x => x,
                    (key, value) => new OrderTypeStatistic {Count = value.Count(), OrderTypeId = key});
        }

        public Task<Dictionary<int, List<AggregatedData>>> GetCountChartDataForPeriod(IEnumerable<int> statuses,
            DateTime start, DateTime end, Expression<Func<Order, AggregatedData>> groupDataSelector,
            ContextSession session)
        {
            return GetChartDataForPeriod(statuses, start, end,
                groupDataSelector,
                group => new AggregatedData {Group = group.Key, Count = group.Count()},
                session);
        }

        public Task<Dictionary<int, List<AggregatedData>>> GetProfitChartDataForPeriod(IEnumerable<int> statuses,
            DateTime start, DateTime end, Expression<Func<Order, AggregatedData>> groupDataSelector,
            ContextSession session)
        {
            return GetChartDataForPeriod(statuses, start, end,
                groupDataSelector,
                group => new AggregatedData {Group = group.Key, Sum = group.Sum(x => x.Sum)},
                session);
        }

        private Task<Dictionary<int, List<AggregatedData>>> GetChartDataForPeriod(
            IEnumerable<int> statuses,
            DateTime start,
            DateTime end,
            Expression<Func<Order, AggregatedData>> groupDataSelector,
            Expression<Func<IGrouping<int, AggregatedData>, AggregatedData>> groupExpression,
            ContextSession session)
        {
            return GetChartData(statuses,
                obj => obj.Date >= start && obj.Date <= end,
                groupDataSelector,
                groupExpression,
                session);
        }

        private async Task<Dictionary<int, List<AggregatedData>>> GetChartData(IEnumerable<int> statuses,
            Expression<Func<Order, bool>> baseFilter,
            Expression<Func<Order, AggregatedData>> groupDataSelector,
            Expression<Func<IGrouping<int, AggregatedData>, AggregatedData>> groupExpression,
            ContextSession session)
        {
            var result = new Dictionary<int, List<AggregatedData>>();

            var context = GetContext(session);

            var dataByGroupsQueries = new Dictionary<int, QueryFutureEnumerable<AggregatedData>>();
            foreach (var status in statuses)
            {
                var query = context.Orders
                    .AsNoTracking()
                    .Where(baseFilter)
                    .Where(obj => status == (int) OrderStatusEnum.All || obj.Status == status)
                    .Select(groupDataSelector)
                    .GroupBy(obj => obj.Group)
                    .Select(groupExpression)
                    .Future();
                dataByGroupsQueries.Add(status, query);
            }

            foreach (var query in dataByGroupsQueries)
            {
                var data = await query.Value.ToListAsync();
                result.Add(query.Key, data);
            }

            return result;
        }
    }
}