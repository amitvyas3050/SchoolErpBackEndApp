using ECommerce.DataAccess.EFCore.Configuration.System;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.EFCore.Configuration
{
    public class RolePermissionConfig : TrackableEntityConfig<RolePermission>
    {
        public RolePermissionConfig() : base("RolePermissions", item => item.RolePermissionCreatedBy, item => item.RolePermissionUpdatedBy)
        { }

        public override void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            base.Configure(builder);
            //builder.Property(obj => obj.RoleId).IsRequired();
            //builder.Property(obj => obj.ModuleId).IsRequired();
            //builder.Property(obj => obj.FeatureId).IsRequired();


            builder.Property(obj => obj.CanAdd).IsRequired();
            builder.Property(obj => obj.CanEdit).IsRequired();
            builder.Property(obj => obj.CanDelete).IsRequired();

            builder.HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId).IsRequired(true);
            builder.HasOne(u => u.AppModule).WithMany().HasForeignKey(u => u.ModuleId).IsRequired(true);
            builder.HasOne(u => u.AppFeature).WithMany().HasForeignKey(u => u.FeatureId).IsRequired(true);

        }


    }
}
