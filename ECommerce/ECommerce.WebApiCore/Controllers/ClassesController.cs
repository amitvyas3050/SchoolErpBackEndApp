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
    [Route("classes")]
    public class ClassesController : BaseApiController
    {
        protected readonly IClassService classService;
        public ClassesController(IClassService classService) : base()
         {
            this.classService = classService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDataForGrid([FromQuery]ClassesGridFilter filter)
        {
            filter = filter ?? new ClassesGridFilter();
            var classes = await classService.GetDataForGrid(filter);
            return Ok(classes);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var classes = await classService.GetById(id);
            return Ok(classes);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(ClassDTO clasDto)
        {
            //if (clasDto.Id != 0)
            //  return BadRequest();

            var result = await classService.Edit(clasDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id, ClassDTO clasDto)
        {
            //if (clasDto.Id != 0)
            //    return BadRequest();

            var result = await classService.Edit(clasDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await classService.Delete(id);
            return Ok(result);
        }
    }
}
