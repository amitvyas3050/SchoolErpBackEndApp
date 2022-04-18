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
    public class OrderConfig : TrackableEntityConfig<Order>
    {
        public OrderConfig() : base("Orders", item => item.OrdersCreatedBy, item => item.OrdersUpdatedBy)
        { }

        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.Name);
            builder.Property(obj => obj.Date).IsRequired();
            builder.Property(obj => obj.Value);
            builder.Property(obj => obj.Currency).IsRequired();
            builder.Property(obj => obj.Type).IsRequired();
            builder.Property(obj => obj.Status).IsRequired();

            builder
                .HasOne(obj => obj.Country)
                .WithMany(item => item.Orders)
                .HasForeignKey(obj => obj.CountryId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
