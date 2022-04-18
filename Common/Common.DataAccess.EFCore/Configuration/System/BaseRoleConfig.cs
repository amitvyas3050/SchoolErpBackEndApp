/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class BaseRoleConfig : BaseEntityConfig<BaseRole>
    {
        public BaseRoleConfig() : base("Roles") { }

        public override void Configure(EntityTypeBuilder<BaseRole> builder)
        {
            base.Configure(builder);
            builder.Property(obj => obj.Name);

            builder
                .HasMany(r => r.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
