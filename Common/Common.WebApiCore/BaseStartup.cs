/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/
using Common.Services.Infrastructure;
using Common.WebApiCore.Identity;
using Common.WebApiCore.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using AutoMapperConfiguration = AutoMapper.Configuration;

namespace Common.WebApiCore
{
    public abstract class BaseStartup
    {
        protected abstract void ConfigureDependencies(IServiceCollection services);
        protected abstract void ConfigureMapping(AutoMapperConfiguration.MapperConfigurationExpression config);
        protected abstract void ConfigureIdentity(IServiceCollection services);

        protected BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            RegisterIdentity(services);
            services.ConfigureAuth(Configuration);
            RegisterDependencies(services);
            RegisterMapping();

            services.ConfigureSwagger();

            services.ConfigureCors();

            services.AddAuthorization(opt => opt.RegisterPolicies());

            services
                .AddControllers(opt =>
                {
                    opt.UseCentralRoutePrefix(new RouteAttribute("api"));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, IDataBaseInitializer dataBaseInitializer)
        {
            if (dataBaseInitializer != null)
            {
                dataBaseInitializer.Initialize();
            }

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseConfiguredSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            BaseDependenciesConfig.ConfigureDependencies(services);
            ConfigureDependencies(services);
        }

        private void RegisterMapping()
        {
            var config = new AutoMapperConfiguration.MapperConfigurationExpression();
            BaseAutoMapperConfig.Configure(config);
            ConfigureMapping(config);
            AutoMapper.Mapper.Initialize(config);
        }

        private void RegisterIdentity(IServiceCollection services)
        {
            BaseIdentityConfig.Configure(services);
            ConfigureIdentity(services);
        }
    }
}
