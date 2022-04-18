/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using ECommerce.DataAccess.EFCore;

namespace Common.DataAccess.EFCore
{
    public class SettingsRepository : BaseRepository<Settings, ECommerceDataContext>, ISettingsRepository
    {
        public SettingsRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}