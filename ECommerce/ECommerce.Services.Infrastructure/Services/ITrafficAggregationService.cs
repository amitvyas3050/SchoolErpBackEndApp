/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using ECommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface ITrafficAggregationService
    {
        Task<IEnumerable<TrafficAggregationDTO>> GetDataForTable(string filter);
        Task<BaseChartDTO<int>> GetDataForChart(string filter);
    }
}
