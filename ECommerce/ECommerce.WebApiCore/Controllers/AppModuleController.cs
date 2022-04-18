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
    [Route("modules")]
    public class AppModuleController : BaseApiController
    {
        protected readonly IAppModuleService moduleService;
        public AppModuleController(IAppModuleService moduleService) : base()
        {
            this.moduleService = moduleService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDataForGrid([FromQuery]ModuleGridFilter filter)
        {
            filter = filter ?? new ModuleGridFilter();
            var modules = await moduleService.GetDataForGrid(filter);
            return Ok(modules);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var modules = await moduleService.GetById(id);
            return Ok(modules);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var modules = await moduleService.GetAll();
            return Ok(modules);
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(AppModuleDTO moduleDto)
        {
            if (moduleDto.Id != 0)
                return BadRequest();

            var result = await moduleService.Edit(moduleDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, AppModuleDTO moduleDto)
        {
            if (id != moduleDto.Id)
                return BadRequest();

            var result = await moduleService.Edit(moduleDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await moduleService.Delete(id);
            return Ok(result);
        }
    }
}
