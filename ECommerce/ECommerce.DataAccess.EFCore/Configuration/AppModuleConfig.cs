using ECommerce.DataAccess.EFCore.Configuration.System;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.EFCore.Configuration
{
    public class AppModuleConfig : TrackableEntityConfig<AppModule>
    {
        public AppModuleConfig() : base("AppModules", item => item.AppModuleCreatedBy, item => item.AppModuleUpdatedBy)
        { }

        public override void Configure(EntityTypeBuilder<AppModule> builder)
        {
            base.Configure(builder);

            builder.Property(obj => obj.Name).IsRequired(); ;
            builder.Property(obj => obj.MenuText).IsRequired(); 
            builder.Property(obj => obj.MenuIcon).IsRequired(); 
            builder.Property(obj => obj.BgColor1).IsRequired();
            builder.Property(obj => obj.BgColor2).IsRequired();
        }
    }
}
