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


    [Route("appfeatures")]
    public class AppFeatureController : BaseApiController
    {
        protected readonly IAppFeatureService appfeatureService;
        public AppFeatureController(IAppFeatureService appfeatureService) : base()
        {
            this.appfeatureService = appfeatureService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDataForGrid([FromQuery] AppFeatureGridFilter filter)
        {
            filter = filter ?? new AppFeatureGridFilter();
            var appfeatures = await appfeatureService.GetDataForGrid(filter);
            return Ok(appfeatures);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var appfeatures = await appfeatureService.GetById(id);
            return Ok(appfeatures);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(AppFeatureDTO appfeatureDto)
        {
            if (appfeatureDto.Id != 0)
                return BadRequest();

            var result = await appfeatureService.Edit(appfeatureDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, AppFeatureDTO appfeatureDto)
        {
            if (id != appfeatureDto.Id)
                return BadRequest();

            var result = await appfeatureService.Edit(appfeatureDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await appfeatureService.Delete(id);
            return Ok(result);
        }
    }




}
