/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.IdentityManagementCore;
using ECommerce.Entities.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.WebApiCore.Setup
{
    public static class IdentityConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.AddTransient<IdentityErrorDescriber>();

            services.AddTransient<IRoleStore<Role>, RoleStore<Role>>();
            services.AddTransient<IUserStore<User>, UserStore<User, Role, UserRole, UserClaim>>();
            services.AddTransient<UserManager<User>, ApplicationUserManager>();
            services.AddTransient(typeof(RoleManager<Role>));

            var identityBuilder = new IdentityBuilder(typeof(User), typeof(User), services);
            identityBuilder.AddDefaultTokenProviders();
        }
    }
}
