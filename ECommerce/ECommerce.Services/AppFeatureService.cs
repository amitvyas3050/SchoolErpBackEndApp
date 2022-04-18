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
    public class AppFeatureService : BaseService, IAppFeatureService
    {
        protected readonly IAppFeatureRepository appfeatureRepository;

        public AppFeatureService(ICurrentContextProvider contextProvider, IAppFeatureRepository appfeatureRepository) : base(contextProvider)
        {
            this.appfeatureRepository = appfeatureRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await appfeatureRepository.Delete(id, Session);
            return true;
        }

        public async Task<AppFeatureDTO> GetById(int id)
        {
            var appFeature = await appfeatureRepository.Get(id, Session);
            return appFeature.MapTo<AppFeatureDTO>();
        }

        public async Task<GridData<AppFeatureDTO>> GetDataForGrid(AppFeatureGridFilter filter)
        {
            var tuple = await appfeatureRepository.GetFilteredListWithTotalCount(filter, Session);

            return new GridData<AppFeatureDTO>
            {
                Items = tuple.Item1.MapTo<IEnumerable<AppFeatureDTO>>(),
                TotalCount = tuple.Item2
            };
        }

        public async Task<AppFeatureDTO> Edit(AppFeatureDTO dto)
        {
            var appFeature = dto.MapTo<AppFeature>();
            //var tempCountry = module.Country;
            //module.Country = null;

            // Set first country as default, cause country is mandatory field
            //if (module.CountryId == 0) module.CountryId = 1;

            var result = await appfeatureRepository.Edit(appFeature, Session);
            //result.Country = tempCountry;
            return result.MapTo<AppFeatureDTO>();
        }
    }
}
