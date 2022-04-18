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
    public class RoleModuleService : BaseService, IRoleModuleService
    {
        protected readonly IRoleModuleRepository rolemoduleRepository;

        public RoleModuleService(ICurrentContextProvider contextProvider, IRoleModuleRepository rolemoduleRepository) : base(contextProvider)
        {
            this.rolemoduleRepository = rolemoduleRepository;
        }
        public async Task<bool> Delete(int id)
        {
            await rolemoduleRepository.Delete(id, Session);
            return true;
        }
        public async Task<RoleModuleDTO> GetById(int id)
        {
            var order = await rolemoduleRepository.Get(id, Session);
            return order.MapTo<RoleModuleDTO>();
        }
        public async Task<IEnumerable<RoleModuleDTO>> GetModulesByRole(int roleID)
        {
            var modules = await rolemoduleRepository.GetModulesForRole(roleID, Session);
            return modules.MapTo<IEnumerable<RoleModuleDTO>>();
        }
        public async Task<GridData<RoleModuleDTO>> GetDataForGrid(RoleModuleGridFilter filter)
        {
            var tuple = await rolemoduleRepository.GetFilteredListWithTotalCount(filter, Session);
            
            return new GridData<RoleModuleDTO>
            {
                Items = tuple.Item1.MapTo<IEnumerable<RoleModuleDTO>>(),
                TotalCount = tuple.Item2
            };
        }
        public async Task<RoleModuleDTO> Edit(RoleModuleDTO dto)
        {
            var roleModule = dto.MapTo<RoleModule>();
            var tempModule = roleModule.Module;
            roleModule.Module = null;

            var tempRole = roleModule.Role;
            roleModule.Role = null;

            // Set first module as default, cause Module is mandatory field
            if (roleModule.ModuleId == 0) roleModule.ModuleId = 1;

            // Set first role as default, cause role is mandatory field
            if (roleModule.RoleId == 0) roleModule.RoleId = 1;

            var result = await rolemoduleRepository.Edit(roleModule, Session);
            result.Module = tempModule;
            result.Role = tempRole;

            return result.MapTo<RoleModuleDTO>();
        }


    }
}
