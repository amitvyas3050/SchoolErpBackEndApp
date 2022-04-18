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
    [Route("traffic-aggregated")]
    public class TrafficAggregatedController : BaseApiController
    {
        protected readonly ITrafficAggregationService trafficAggregationService;
        public TrafficAggregatedController(ITrafficAggregationService trafficAggregationService) : base()
        {
            this.trafficAggregationService = trafficAggregationService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> TrafficStatistics(string filter = "year")
        {
            var data = await trafficAggregationService.GetDataForTable(filter);
            return Ok(data);
        }
    }
}
