/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.DataAccess.EFCore.Configuration;
using ECommerce.DataAccess.EFCore.Configuration;
using ECommerce.DataAccess.EFCore.Configuration.System;
using ECommerce.Entities;
using ECommerce.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.EFCore
{
    public class ECommerceDataContext : CommonDataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Traffic> Traffic { get; set; }
        public DbSet<Class> Class { get; set; }


        public DbSet<AppModule> AppModules { get; set; }
        public DbSet<AppFeature> AppFeatures { get; set; }
        public DbSet<RoleModule> RoleModules { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        public ECommerceDataContext(DbContextOptions<ECommerceDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new UserClaimConfig());
            modelBuilder.ApplyConfiguration(new SettingsConfig());

            modelBuilder.ApplyConfiguration(new CountryConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new UserActivityConfig());
            modelBuilder.ApplyConfiguration(new TrafficConfig());

            modelBuilder.ApplyConfiguration(new ClassConfig());

            modelBuilder.ApplyConfiguration(new AppModuleConfig());
            modelBuilder.ApplyConfiguration(new AppFeatureConfig());
            modelBuilder.ApplyConfiguration(new RoleModuleConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());

            modelBuilder.HasDefaultSchema("ecom_core");
        }
    }

}
