using Common.WebApiCore.Controllers;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.WebApiCore.Controllers
{
    [Route("rolemodules")]
    public class RoleModuleController : BaseApiController
    {
        protected readonly IRoleModuleService rolemoduleService;
        public RoleModuleController(IRoleModuleService rolemoduleService) : base()
         {
            this.rolemoduleService = rolemoduleService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDataForGrid([FromQuery]RoleModuleGridFilter filter)
        {
            filter = filter ?? new RoleModuleGridFilter();
            var rolemodules = await rolemoduleService.GetDataForGrid(filter);
            return Ok(rolemodules);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var rolemodules = await rolemoduleService.GetById(id);
            return Ok(rolemodules);
        }
        [HttpGet]
        [Route("getmodulesforrole/{id:int}")]
        public async Task<IActionResult> GetModulesForRole(int id)
        {
            var rolemodules = await rolemoduleService.GetModulesByRole(id);
            return Ok(rolemodules);
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(RoleModuleDTO rolemoduleDto)
        {
            if (rolemoduleDto.Id != 0)
                return BadRequest();

            var result = await rolemoduleService.Edit(rolemoduleDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, RoleModuleDTO rolemoduleDto)
        {
            if (id != rolemoduleDto.Id)
                return BadRequest();

            var result = await rolemoduleService.Edit(rolemoduleDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await rolemoduleService.Delete(id);
            return Ok(result);
        }
    }
}
