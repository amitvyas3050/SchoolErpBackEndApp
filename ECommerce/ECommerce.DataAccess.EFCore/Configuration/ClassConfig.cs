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
    public class ClassConfig : TrackableEntityConfig<Class>
    {
        public ClassConfig() : base("Class", item => item.ClassCreatedBy, item => item.ClassUpdatedBy)
        { }

        public override void Configure(EntityTypeBuilder<Class> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.ClassName).IsRequired();
            builder.Property(obj => obj.Description);


        }
    }
}
