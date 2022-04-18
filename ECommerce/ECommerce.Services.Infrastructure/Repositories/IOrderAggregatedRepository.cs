/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using ECommerce.Entities;
using ECommerce.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IOrderAggregatedRepository
    {
        Task<Dictionary<int, List<AggregatedData>>> GetCountChartDataForPeriod(IEnumerable<int> statuses,
            DateTime start, DateTime end, Expression<Func<Order, AggregatedData>> groupDataSelector,
            ContextSession session);

        Task<Dictionary<int, List<AggregatedData>>> GetProfitChartDataForPeriod(IEnumerable<int> statuses,
            DateTime start, DateTime end, Expression<Func<Order, AggregatedData>> groupDataSelector,
            ContextSession session);

        Task<IEnumerable<AggregatedData<DateTime>>> GetProfitForDateRangeChartData(ContextSession session,
            DateTime start, DateTime end);

        Task<IEnumerable<OrderTypeStatistic>> GetDataGroupedByCountry(string code, ContextSession session);
        Task<OrdersProfit> GetProfitInfo(ContextSession session);
        Task<OrdersSummary> GetOrdersSummaryInfo(ContextSession session);
    }
}
