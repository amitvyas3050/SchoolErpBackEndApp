/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IClassRepository
    {
        Task<(IEnumerable<Class>, int)> GetFilteredListWithTotalCount(ClassesGridFilter filter, ContextSession session);
        Task<Class> Get(int id, ContextSession session);
        Task<Class> Edit(Class classes, ContextSession session);
        Task Delete(int id, ContextSession session);
    }
}
