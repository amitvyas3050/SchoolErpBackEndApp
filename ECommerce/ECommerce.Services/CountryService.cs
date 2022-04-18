/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Services;
using Common.Services.Infrastructure;
using Common.Utils;
using ECommerce.DTO;
using ECommerce.Services.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class CountryService : BaseService, ICountryService
    {
        protected readonly ICountryRepository countryRepository;

        public CountryService(ICurrentContextProvider contextProvider, ICountryRepository countryRepository) : base(contextProvider)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<CountryDTO> GetById(int id)
        {
            var country = await countryRepository.Get(id, Session);
            return country.MapTo<CountryDTO>();
        }

        public async Task<IEnumerable<CountryDTO>> GetList()
        {
            var countries = await countryRepository.GetList(Session);
            return countries.MapTo<IEnumerable<CountryDTO>>();
        }
    }
}
