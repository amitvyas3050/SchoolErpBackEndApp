/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.DataAccess.EFCore.Configuration.System;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.EFCore.Configuration
{
    public class AppFeatureConfig : TrackableEntityConfig<AppFeature>
    {
        public AppFeatureConfig() : base("AppFeatures", item => item.AppFeatureCreatedBy, item => item.AppFeatureUpdatedBy)
        { }

        public override void Configure(EntityTypeBuilder<AppFeature> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.Name).IsRequired(); ;
            builder.Property(obj => obj.MenuText); 
            builder.Property(obj => obj.MenuIcon); 
            builder.Property(obj => obj.BgColor1);
            builder.Property(obj => obj.BgColor2);
            builder.Property(obj => obj.NavUrl);
            builder
                .HasOne(obj => obj.Module)
                .WithMany(item => item.Features)
                .HasForeignKey(obj => obj.ModuleId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
