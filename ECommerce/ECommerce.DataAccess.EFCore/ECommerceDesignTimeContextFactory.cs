﻿/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ECommerce.DataAccess.EFCore
{
    class ECommerceDesignTimeContextFactory : IDesignTimeDbContextFactory<ECommerceDataContext>
    {
        // this class used by EF tool for creating migrations via Package Manager Console
        // Add-Migration MigrationName -Project ECommerce.DataAccess.EFCore -StartupProject ECommerce.DataAccess.EFCore
        // or in case of console building: dotnet ef migrations add MigrationName --project ECommerce.DataAccess.EFCore --startup-project ECommerce.DataAccess.EFCore
        // after migration is added, please add migrationBuilder.Sql(SeedData.Initial()); to the end of seed method for initial data
        public ECommerceDesignTimeContextFactory() {}

        public ECommerceDataContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ECommerce.WebApiCore"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("localDb");

            var builder = new DbContextOptionsBuilder<ECommerceDataContext>();
            builder.UseSqlServer(connectionString);

            return new ECommerceDataContext(builder.Options);
        }
    }
}
