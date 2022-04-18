
using Common.Entities.System;
using ECommerce.DTO;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IRoleModuleService
    {
        Task<GridData<RoleModuleDTO>> GetDataForGrid(RoleModuleGridFilter filter);
        Task<RoleModuleDTO> GetById(int id);
        Task<bool> Delete(int id);
        Task<RoleModuleDTO> Edit(RoleModuleDTO dto);
        Task<IEnumerable<RoleModuleDTO>> GetModulesByRole(int roleID);
    }
}
