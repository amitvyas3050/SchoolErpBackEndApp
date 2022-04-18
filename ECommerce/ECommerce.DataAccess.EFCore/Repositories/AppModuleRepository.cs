
using Common.DataAccess.EFCore;
using Common.Entities;
using ECommerce.DataAccess.EFCore.Extensions;
using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFCore
{
    public class AppModuleRepository : BaseRepository<AppModule, ECommerceDataContext>, IAppModuleRepository
    {
        public AppModuleRepository(ECommerceDataContext context) : base(context)
        {
        }

        public override async Task<AppModule> Get(int id, ContextSession session)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .FirstOrDefaultAsync();
        }
        public  async Task<IEnumerable<AppModule>> GetAll(ContextSession session)
        {
            return await GetEntities(session).ToListAsync();
                
        }



        public async Task<(IEnumerable<AppModule>, int)> GetFilteredListWithTotalCount(ModuleGridFilter filter,
            ContextSession session)
        {
            var query = GetEntities(session).ApplyFilter(filter);

            return (
                await query
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    
                    .ToArrayAsync(),
                await query
                    .CountAsync());
        }
    }
}