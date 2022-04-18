/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore.Controllers;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.WebApiCore.Controllers
{
    [Route("orders")]
    public class OrdersController : BaseApiController
    {
        protected readonly IOrderService orderService;
        public OrdersController(IOrderService orderService) : base()
         {
            this.orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDataForGrid([FromQuery]OrdersGridFilter filter)
        {
            filter = filter ?? new OrdersGridFilter();
            var orders = await orderService.GetDataForGrid(filter);
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await orderService.GetById(id);
            return Ok(order);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(OrderDTO orderDto)
        {
            if (orderDto.Id != 0)
                return BadRequest();

            var result = await orderService.Edit(orderDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, OrderDTO orderDto)
        {
            if (id != orderDto.Id)
                return BadRequest();

            var result = await orderService.Edit(orderDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await orderService.Delete(id);
            return Ok(result);
        }
    }
}
