/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.WebApiCore;
using ECommerce.WebApiCore.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapperConfiguration = AutoMapper.Configuration;

namespace ECommerce.WebApiCore
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        protected override void ConfigureDependencies(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("localDb");
            DependenciesConfig.ConfigureDependencies(services, connectionString);
        }

        protected override void ConfigureIdentity(IServiceCollection services)
        {
            IdentityConfig.Configure(services);
        }

        protected override void ConfigureMapping(AutoMapperConfiguration.MapperConfigurationExpression config)
        {
            AutoMapperConfig.Configure(config);
        }
    }
}
