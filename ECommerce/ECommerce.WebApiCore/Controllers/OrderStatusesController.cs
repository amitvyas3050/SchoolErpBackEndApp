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
    [Route("order-statuses")]
    public class OrderStatusesController : BaseApiController
    {
        protected readonly IOrderStatusService orderStatusService;
        public OrderStatusesController(IOrderStatusService orderStatusService)
        {
            this.orderStatusService = orderStatusService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var orderTypes = await orderStatusService.GetList();
            return Ok(orderTypes);
        }
    }
}