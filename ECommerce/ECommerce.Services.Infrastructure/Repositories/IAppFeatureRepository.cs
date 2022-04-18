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
    public interface IAppFeatureRepository
    {
        Task<(IEnumerable<AppFeature>, int)> GetFilteredListWithTotalCount(AppFeatureGridFilter filter, ContextSession session);
        Task<AppFeature> Get(int id, ContextSession session);
        Task<AppFeature> Edit(AppFeature module, ContextSession session);
        Task Delete(int id, ContextSession session);

        Task<IEnumerable<AppFeature>> GetFeatureForModule(int moduleID, ContextSession session);
    }
}
