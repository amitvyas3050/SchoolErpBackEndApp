using ECommerce.DataAccess.EFCore.Configuration.System;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.EFCore.Configuration
{
    public class RoleModuleConfig : TrackableEntityConfig<RoleModule>
    {
        public RoleModuleConfig() : base("RoleModules", item => item.RoleModuleCreatedBy, item => item.RoleModuleUpdatedBy)
        { }

        public override void Configure(EntityTypeBuilder<RoleModule> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.RoleId).IsRequired(); 
            builder.Property(obj => obj.ModuleId).IsRequired();
            builder.Ignore(x => x.Role);
            builder.Ignore(x => x.Module);
            builder.Property(obj => obj.NavUrl).IsRequired();

            //builder
            //    .HasOne(obj => obj.Role)
            //    .WithMany(item => item.UserRoles)
            //    .HasForeignKey(obj => obj)
            //    .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            //builder
            //   .HasOne(obj => obj.Module)
            //   .WithMany(item => item.)
            //   .HasForeignKey(obj => obj.ModuleId)
            //   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            //builder
            //  .HasOne(obj => obj.Role)
            //  .WithMany(item => item.RoleModules)
            //  .HasForeignKey(obj => obj.RoleId)
            //  .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
