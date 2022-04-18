/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using ECommerce.DTO;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IRolePermissionRepository
    {
        Task<(IEnumerable<RolePermission>, int)> GetFilteredListWithTotalCount(RolePermissionGridFilter filter, ContextSession session);
        Task<RolePermission> Get(int id, ContextSession session);
        Task<RolePermission> Edit(RolePermission rolemodule, ContextSession session);
        Task Delete(int id, ContextSession session);

        Task<IEnumerable<RoleModulesDTO>> GetAllModulesList(int roleID, ContextSession session);
    }
}
