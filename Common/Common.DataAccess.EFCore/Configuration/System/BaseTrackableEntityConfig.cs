/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public abstract class BaseTrackableEntityConfig<TType> : BaseEntityConfig<TType> 
        where TType : BaseTrackableEntity
    {
        public BaseTrackableEntityConfig(string tableName) : base(tableName)
        { }

        public override void Configure(EntityTypeBuilder<TType> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.CreatedByUserId).IsRequired();
            builder.Property(obj => obj.CreatedDate).IsRequired();
            builder.Property(obj => obj.UpdatedByUserId);
            builder.Property(obj => obj.UpdatedDate);
        }
    }
}
