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
    public class ClassService : BaseService, IClassService
    {
        protected readonly IClassRepository classRepository;

        public ClassService(ICurrentContextProvider contextProvider, IClassRepository classRepository) : base(contextProvider)
        {
            this.classRepository = classRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await classRepository.Delete(id, Session);
            return true;
        }

        public async Task<ClassDTO> GetById(int id)
        {
            var calss = await classRepository.Get(id, Session);
            return calss.MapTo<ClassDTO>();
        }

        public async Task<GridData<ClassDTO>> GetDataForGrid(ClassesGridFilter filter)
        {
            var tuple = await classRepository.GetFilteredListWithTotalCount(filter, Session);
            
            return new GridData<ClassDTO>
            {
                Items = tuple.Item1.MapTo<IEnumerable<ClassDTO>>(),
                TotalCount = tuple.Item2
            };
        }

        public async Task<ClassDTO> Edit(ClassDTO dto)
        {
            var calss = dto.MapTo<Class>();
            

            // Set first country as default, cause country is mandatory field
            //if (order.CountryId == 0) order.CountryId = 1;

            var result = await classRepository.Edit(calss, Session);
            //result.Country = tempCountry;
            return result.MapTo<ClassDTO>();
        }
    }
}
