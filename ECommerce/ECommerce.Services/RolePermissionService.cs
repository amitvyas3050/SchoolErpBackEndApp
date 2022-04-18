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
    public class RolePermissionService : BaseService, IRolePermissionService
    {
        protected readonly IRolePermissionRepository rolepermissionRepository;
        


        public RolePermissionService(ICurrentContextProvider contextProvider, IRolePermissionRepository rolepermissionRepository) : base(contextProvider)
        {
            this.rolepermissionRepository = rolepermissionRepository;
            
        }

        public async Task<bool> Delete(int id)
        {
            await rolepermissionRepository.Delete(id, Session);
            return true;
        }

        public async Task<RolePermissionDTO> GetById(int id)
        {
            var order = await rolepermissionRepository.Get(id, Session);
            return order.MapTo<RolePermissionDTO>();
        }



        public async Task<GridData<RolePermissionDTO>> GetDataForGrid(RolePermissionGridFilter filter)
        {
            var tuple = await rolepermissionRepository.GetFilteredListWithTotalCount(filter, Session);
            
            return new GridData<RolePermissionDTO>
            {
                Items = tuple.Item1.MapTo<IEnumerable<RolePermissionDTO>>(),
                TotalCount = tuple.Item2
            };
        }

        public async Task<IEnumerable<RoleModulesDTO>> GetAllModulesAndFeaturesForRole(int RoleId)
        {
            var modulelist = await rolepermissionRepository.GetAllModulesList(RoleId, Session);
            return modulelist;

        }



        

        public async Task<RolePermissionDTO> Edit(RolePermissionDTO dto)
        {
            var rolePermission = dto.MapTo<RolePermission>();
            var tempModule = rolePermission.AppModule;
            rolePermission.AppModule = null;

            var tempRole = rolePermission.Role;
            rolePermission.Role = null;

            // Set first module as default, cause Module is mandatory field
            if (rolePermission.ModuleId == 0) rolePermission.ModuleId = 1;

            // Set first role as default, cause role is mandatory field
            if (rolePermission.RoleId == 0) rolePermission.RoleId = 1;

            var result = await rolepermissionRepository.Edit(rolePermission, Session);
            result.AppModule = tempModule;
            result.Role = tempRole;

            return result.MapTo<RolePermissionDTO>();
        }


    }
}
