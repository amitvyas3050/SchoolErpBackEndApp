/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class BaseUserClaimConfig : BaseEntityConfig<BaseUserClaim>
    {
        public BaseUserClaimConfig() : base("UserClaims") { }

        public override void Configure(EntityTypeBuilder<BaseUserClaim> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.ClaimType).IsRequired();
            builder.Property(obj => obj.ClaimValue).IsRequired();

            builder.Ignore(obj => obj.User);
        }
    }
}
