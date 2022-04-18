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
    [Route("user-activity")]
    public class UserActivitiesController : BaseApiController
    {
        protected readonly IUserActivityAggregationService userActivityService;
        public UserActivitiesController(IUserActivityAggregationService userActivityService) : base()
        {
            this.userActivityService = userActivityService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> UserActivities(string date = "year")
        {
            var result = await this.userActivityService.GetDataForTable(date);
            return Ok(result);
        }
    }
}
