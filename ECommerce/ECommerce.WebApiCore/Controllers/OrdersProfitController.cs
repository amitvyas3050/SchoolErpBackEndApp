/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore.Controllers;
using ECommerce.Services.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.WebApiCore.Controllers
{
    [Route("orders-profit")]
    public class OrdersProfitController : BaseApiController
    {
        protected readonly IOrderAggregationService orderAggregationService;
        public OrdersProfitController(IOrderAggregationService orderAggregationService) : base()
        {
            this.orderAggregationService = orderAggregationService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProfitChartForYear()
        {
            var ordersProfit = await orderAggregationService.GetProfitChartForYear();
            return Ok(ordersProfit);
        }

        [HttpGet]
        [Route("short")]
        public async Task<IActionResult> GetProfitChartForTwoMonth()
        {
            var ordersProfit = await orderAggregationService.GetProfitChartForTwoMonth();
            return Ok(ordersProfit);
        }

        [HttpGet]
        [Route("summary")]
        [Authorize]
        public async Task<IActionResult> GetProfitSummary()
        {
            var ordersProfit = await orderAggregationService.GetProfitStatistic();
            return Ok(ordersProfit);
        }
    }
}
