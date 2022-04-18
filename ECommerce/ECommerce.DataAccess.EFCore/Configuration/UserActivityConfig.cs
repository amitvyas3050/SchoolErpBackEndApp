/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore.Configuration.System;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.EFCore.Configuration
{
    public class UserActivityConfig : BaseEntityConfig<UserActivity>
    {
        public UserActivityConfig() : base("UserActivities")
        { }

        public override void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.UserId).IsRequired();
            builder.Property(obj => obj.Date).IsRequired();
            builder.Property(obj => obj.Url).IsRequired();

            builder
                .HasOne(obj => obj.User)
                .WithMany(item => item.UserActivities)
                .HasForeignKey(obj => obj.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
