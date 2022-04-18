/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore.Controllers;
using ECommerce.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.WebApiCore.Controllers
{
    [Route("orders-aggregated")]
    public class OrdersAggregatedController : BaseApiController
    {
        protected readonly IOrderAggregationService orderAggregationService;
        public OrdersAggregatedController(IOrderAggregationService orderAggregationService) : base()
        {
            this.orderAggregationService = orderAggregationService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCountDataForChart(string aggregation = "year")
        {
            var dataForGraphic = await orderAggregationService.GetCountDataForChart(aggregation);
            return Ok(dataForGraphic);
        }

        [HttpGet]
        [Route("profit")]
        public async Task<IActionResult> GetProfitDataForChart(string aggregation = "year")
        {
            var dataForGraphic = await orderAggregationService.GetProfitDataForChart(aggregation);
            return Ok(dataForGraphic);
        }

        [HttpGet]
        [Route("country")]
        public async Task<IActionResult> GetStatisticByCountry(string countryCode)
        {
            var data = await orderAggregationService.GetStatisticByCountry(countryCode);
            return Ok(data);
        }

        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> GetOrdersSummaryInfo()
        {
            var ordersSummaryInfo = await orderAggregationService.GetOrdersSummaryInfo();
            return Ok(ordersSummaryInfo);
        }
    }
}
