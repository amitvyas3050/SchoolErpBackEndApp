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
    public class AppModuleService : BaseService, IAppModuleService
    {
        protected readonly IAppModuleRepository moduleRepository;

        public AppModuleService(ICurrentContextProvider contextProvider, IAppModuleRepository moduleRepository) : base(contextProvider)
        {
            this.moduleRepository = moduleRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await moduleRepository.Delete(id, Session);
            return true;
        }

        public async Task<AppModuleDTO> GetById(int id)
        {
            var module = await moduleRepository.Get(id, Session);
            return module.MapTo<AppModuleDTO>();
        }
        public async Task<IEnumerable<AppModuleDTO>> GetAll()
        {
            var module = await moduleRepository.GetAll(Session);
            return module.MapTo<IEnumerable<AppModuleDTO>>();
        }

        public async Task<GridData<AppModuleDTO>> GetDataForGrid(ModuleGridFilter filter)
        {
            var tuple = await moduleRepository.GetFilteredListWithTotalCount(filter, Session);

            return new GridData<AppModuleDTO>
            {
                Items = tuple.Item1.MapTo<IEnumerable<AppModuleDTO>>(),
                TotalCount = tuple.Item2
            };
        }

        public async Task<AppModuleDTO> Edit(AppModuleDTO dto)
        {
            var module = dto.MapTo<AppModule>();
            //var tempCountry = module.Country;
            //module.Country = null;

            // Set first country as default, cause country is mandatory field
            //if (module.CountryId == 0) module.CountryId = 1;

            var result = await moduleRepository.Edit(module, Session);
            //result.Country = tempCountry;
            return result.MapTo<AppModuleDTO>();
        }
    }
}
