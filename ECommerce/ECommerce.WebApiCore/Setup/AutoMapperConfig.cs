/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper.Configuration;
using ECommerce.Services.Infrastructure.MappingProfiles;

namespace ECommerce.WebApiCore.Setup
{
    public static class AutoMapperConfig
    {
        public static void Configure(MapperConfigurationExpression config)
        {
            config.AddProfile<ClassProfile>();
            config.AddProfile<OrderProfile>();
            config.AddProfile<UserProfile>();

            config.AddProfile<AppModuleProfile>();
            config.AddProfile<AppFeatureProfile>();
            config.AddProfile<RoleModuleProfile>();
            config.AddProfile<RolePermissionProfile>();
        }
    }
}
