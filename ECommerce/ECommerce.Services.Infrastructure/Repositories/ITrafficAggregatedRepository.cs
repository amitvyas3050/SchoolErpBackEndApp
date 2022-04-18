/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Entities.Statistics;
using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface ITrafficAggregatedRepository
    {
        Task<IEnumerable<AggregatedData>> GetDataByPeriod(DateTime start, DateTime end, Expression<Func<Traffic, AggregatedData>> groupDataSelector, ContextSession session);
    }
}
