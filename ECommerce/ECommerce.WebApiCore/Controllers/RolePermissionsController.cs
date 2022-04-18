/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore.Controllers;
using ECommerce.DTO;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.WebApiCore.Controllers
{
    [Route("rolepermission")]
    public class RolePermissionController : BaseApiController
    {
        protected readonly IRolePermissionService rolepermissionService;
        public RolePermissionController(IRolePermissionService rolepermissionService) : base()
        {
            this.rolepermissionService = rolepermissionService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDataForGrid([FromQuery]RolePermissionGridFilter filter)
        {
            filter = filter ?? new RolePermissionGridFilter();
            var rolepermissions = await rolepermissionService.GetDataForGrid(filter);
            return Ok(rolepermissions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var rolepermissions = await rolepermissionService.GetById(id);
            return Ok(rolepermissions);
        }

        [HttpGet]
        [Route("gealltmodulesandfeatures/{id:int}")]
        public async Task<IActionResult> GetModulesForRole(int id)
        {
            var rolemodules = await rolepermissionService.GetAllModulesAndFeaturesForRole(id);
            return Ok(rolemodules);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(RolePermissionDTO rolepermissionDto)
        {
            if (rolepermissionDto.Id != 0)
                return BadRequest();

            var result = await rolepermissionService.Edit(rolepermissionDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, RolePermissionDTO rolepermissionDto)
        {
            if (id != rolepermissionDto.Id)
                return BadRequest();

            var result = await rolepermissionService.Edit(rolepermissionDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await rolepermissionService.Delete(id);
            return Ok(result);
        }
    }
}
