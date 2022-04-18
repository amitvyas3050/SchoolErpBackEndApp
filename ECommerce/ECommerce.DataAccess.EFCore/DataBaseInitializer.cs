/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.EFCore
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        private ECommerceDataContext context;
        public DataBaseInitializer(ECommerceDataContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            using (context)
            {
                // turn off timeout for initial seeding
                context.Database.SetCommandTimeout(System.TimeSpan.FromDays(1));
                // check data base version and migrate / seed if needed
                context.Database.Migrate();
            }
        }
    }
}
