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
    public class TrafficConfig : BaseEntityConfig<Traffic>
    {
        public TrafficConfig() : base("Traffic")
        { }

        public override void Configure(EntityTypeBuilder<Traffic> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.Value).IsRequired();
            builder.Property(obj => obj.Date).IsRequired();
        }
    }
}
