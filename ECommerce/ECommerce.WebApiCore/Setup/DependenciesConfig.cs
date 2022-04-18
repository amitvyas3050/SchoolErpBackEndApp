/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore;
using Common.WebApiCore.Identity;
using ECommerce.DIContainerCore;
using ECommerce.Entities.System;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.WebApiCore.Setup
{
    public class DependenciesConfig
    {
        public static void ConfigureDependencies(IServiceCollection services, string connectionString)
        {
            ContainerExtension.Initialize(services, connectionString);

            services.AddTransient<IAuthenticationService, AuthenticationService<User>>();
            services.AddTransient<IRoleService, RoleService<User, Role>>();
        }
    }
}
