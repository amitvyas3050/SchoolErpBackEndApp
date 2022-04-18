/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities.System;
using ECommerce.DTO;
using ECommerce.Entities;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IOrderService
    {
        Task<GridData<OrderDTO>> GetDataForGrid(OrdersGridFilter filter);
        Task<OrderDTO> GetById(int id);
        Task<bool> Delete(int id);
        Task<OrderDTO> Edit(OrderDTO dto);
    }
}
