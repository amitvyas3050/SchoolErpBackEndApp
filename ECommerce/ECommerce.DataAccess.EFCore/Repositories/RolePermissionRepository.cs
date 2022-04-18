/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.Entities;
using ECommerce.DataAccess.EFCore.Extensions;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFCore
{
    public class RolePermissionRepository : BaseRepository<RolePermission, ECommerceDataContext>, IRolePermissionRepository
    {
        ECommerceDataContext _context;
        IRoleModuleRepository _rolemoduleRepository;
        IAppFeatureRepository _featureRepository;
        public RolePermissionRepository(ECommerceDataContext context) : base(context)
        {
            _context = context;
        }

        public RolePermissionRepository(ECommerceDataContext context, RoleModuleRepository rolemoduleRepository, AppFeatureRepository featureRepository) : base(context)
        {
            _rolemoduleRepository = rolemoduleRepository;
            _featureRepository = featureRepository;
            _context = context;
        }

        public override async Task<RolePermission> Get(int id, ContextSession session)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .Include(c => c.AppModule)
                .Include(c => c.Role)
                .Include(c => c.AppFeature)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<RolePermission>, int)> GetFilteredListWithTotalCount(RolePermissionGridFilter filter,ContextSession session)
        {
            var query = GetEntities(session).ApplyFilter(filter);

            return (
                await query
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .Include(c => c.AppModule)
                    .Include(c => c.Role)
                    .Include(c => c.AppFeature)
                    .ToArrayAsync(),
                await query
                    .CountAsync());



        }

        //public Task<IEnumerable<AppModule>> GetModulesForRole(int roleID)//not required here
        //{

        //}
        //public Task<IEnumerable<AppFeature>> GetFeaturesAssginedForModuleRole(int roleID, int moduleID)
        //{

        //}
        //public async Task<RoleModuleDTO> GetRolewiseModulesWithFeatures(int roleId)
        //{
        //    RoleModulesDTO list = new RoleModulesDTO();

        //}
        public async Task<IEnumerable<RoleModulesDTO>> GetAllModulesList(int roleID, ContextSession session)
        {
           
           //List<RoleModulesDTO> list = new List<RoleModulesDTO>(); // old statement

            List<RoleModulesDTO> list = new List<RoleModulesDTO>();

            var modules  = await _rolemoduleRepository.GetModulesForRole(roleID, session);

            foreach (RoleModule objrmodule in modules)
            {
                RoleModulesDTO moduledto = new RoleModulesDTO();
                moduledto.Id = objrmodule.Module.Id;
                moduledto.Name = objrmodule.Module.Name;
                moduledto.MenuText = objrmodule.Module.MenuText;
                moduledto.MenuIcon = objrmodule.Module.MenuIcon;
                moduledto.BgColor1 = objrmodule.Module.BgColor1;
                moduledto.BgColor2 = objrmodule.Module.BgColor2;
                moduledto.NavUrl = objrmodule.Module.Name ;

                moduledto.Items = new List<ModuleFeatureDTO>();

                var features = await _featureRepository.GetFeatureForModule(moduledto.Id, session);

                foreach (AppFeature objfeature in features)
                {
                    ModuleFeatureDTO featureDTO = new ModuleFeatureDTO();
                    featureDTO.Id = objfeature.Id;
                    featureDTO.Name = objfeature.Name;
                    featureDTO.MenuText = objfeature.MenuText;
                    featureDTO.MenuIcon = objfeature.MenuIcon;
                    featureDTO.BgColor1 = objfeature.BgColor1;
                    featureDTO.BgColor2 = objfeature.BgColor2;
                    featureDTO.NavUrl = objfeature.NavUrl;
                    moduledto.Items.Add(featureDTO);
                }
                

                list.Add(moduledto);
            }

            //list = await _rolemoduleRepository.GetModulesForRole(roleID, session);
            //list = modules.MapTo<IEnumerable<RoleModuleDTO>>();

            //var features = await _featureRepository.Get(roleID, session);


            return list;

        }



    }
}