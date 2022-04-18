/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class OrderTypeService : IOrderTypeService
    {
        public async Task<IEnumerable<string>> GetList()
        {
            return await Task.FromResult(
                Enum.GetValues(typeof(OrderTypeEnum)).Cast<int>()
                    .Select(x => ((OrderTypeEnum)x).ToString())
            );
        }
    }
}
