/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities.System;
using Common.Services;
using Common.Services.Infrastructure;
using Common.Utils;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class OrderService : BaseService, IOrderService
    {
        protected readonly IOrderRepository orderRepository;

        public OrderService(ICurrentContextProvider contextProvider, IOrderRepository orderRepository) : base(contextProvider)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await orderRepository.Delete(id, Session);
            return true;
        }

        public async Task<OrderDTO> GetById(int id)
        {
            var order = await orderRepository.Get(id, Session);
            return order.MapTo<OrderDTO>();
        }

        public async Task<GridData<OrderDTO>> GetDataForGrid(OrdersGridFilter filter)
        {
            var tuple = await orderRepository.GetFilteredListWithTotalCount(filter, Session);
            
            return new GridData<OrderDTO>
            {
                Items = tuple.Item1.MapTo<IEnumerable<OrderDTO>>(),
                TotalCount = tuple.Item2
            };
        }

        public async Task<OrderDTO> Edit(OrderDTO dto)
        {
            var order = dto.MapTo<Order>();
            var tempCountry = order.Country;
            order.Country = null;

            // Set first country as default, cause country is mandatory field
            if (order.CountryId == 0) order.CountryId = 1;

            var result = await orderRepository.Edit(order, Session);
            result.Country = tempCountry;
            return result.MapTo<OrderDTO>();
        }
    }
}
