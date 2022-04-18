/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IUserActivityAggregatedRepository
    {
        Task<List<AggregatedData>> GetYearlyDataForTable(ContextSession session, DateTime yearsBefore);
        Task<List<AggregatedData>> GetMonthDataForTable(ContextSession session);
        Task<List<AggregatedData>> GetWeekDataForTable(ContextSession session, DateTime startOfPreviousWeek, DateTime endOfPreviousWeek);
    }
}
