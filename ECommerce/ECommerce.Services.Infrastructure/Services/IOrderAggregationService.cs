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
    public interface IOrderAggregationService
    {
        Task<BaseChartDTO<int>> GetCountDataForChart(string aggregation);
        Task<BaseChartDTO<int>> GetProfitDataForChart(string aggregation);
        Task<OrdersSummaryDTO> GetOrdersSummaryInfo();

        Task<OrdersProfitDTO> GetProfitStatistic();
        Task<ProfitForTwoMonthChartDTO> GetProfitChartForTwoMonth();
        Task<BaseChartDTO<int>> GetProfitChartForYear();

        Task<IEnumerable<OrderTypeStatisticDTO>> GetStatisticByCountry(string code);
    }
}
