/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.Services;
using Common.Services.Infrastructure;
using ECommerce.DataAccess.EFCore;
using ECommerce.Entities.System;
using ECommerce.Services;
using ECommerce.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, string connectionString)
        {
            // setup injection of EF context with correct connection to data base
            services.AddDbContext<ECommerceDataContext>(options => options.UseSqlServer(connectionString));
            // setup injection for data base initialization
            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderAggregatedRepository, OrderAggregatedRepository>();
            services.AddTransient<IUserActivityAggregationService, UserActivityAggregationService>();
            services.AddTransient<IUserActivityAggregatedRepository, UserActivityAggregatedRepository>();
            services.AddTransient<IOrderTypeService, OrderTypeService>();
            services.AddTransient<IOrderAggregationService, OrderAggregationService>();
            services.AddTransient<ITrafficAggregationService, TrafficAggregationService>();
            services.AddTransient<ITrafficAggregatedRepository, TrafficAggregatedRepository>();
            services.AddTransient<IUserPhotoRepository, UserPhotoRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IUserService, UserService<User>>();
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IIdentityUserRepository<User>, IdentityUserRepository>();
            services.AddTransient<IRoleRepository<Role>, RoleRepository>();
            services.AddTransient<IUserRoleRepository<UserRole>, UserRoleRepository>();
            services.AddTransient<IUserClaimRepository<UserClaim>, UserClaimRepository>();


            services.AddTransient<IClassRepository, ClassRepository>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();

            #region APP ROLE MODULE FEATURES & PERMISSONS
            services.AddTransient<IAppModuleRepository, AppModuleRepository>();
            services.AddTransient<IAppModuleService, AppModuleService>();

            services.AddTransient<IAppFeatureRepository, AppFeatureRepository>();
            services.AddTransient<IAppFeatureService, AppFeatureService>();

            services.AddTransient<IRoleModuleRepository, RoleModuleRepository>();
            services.AddTransient<IRoleModuleService, RoleModuleService>();

            services.AddTransient<IRolePermissionRepository, RolePermissionRepository>();
            //services.AddTransient<IRoleModuleRepository, RolePermissionRepository>();
            services.AddTransient<IRolePermissionService, RolePermissionService>();
            #endregion






        }
    }
}
