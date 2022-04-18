using Common.DTO;
using System;
using System.Collections.Generic;

namespace ECommerce.DTO
{
    public class RolePermissionDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual RoleDTO Role { get; set; }

        public int ModuleId { get; set; }
        public virtual AppModuleDTO Module { get; set; }

        public int FeatureId { get; set; }
        public virtual AppFeatureDTO AppFeature { get; set; }

        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }


    }

    public class RoleModulesDTO
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string MenuText { get; set; }
        public string MenuIcon { get; set; }
        public string BgColor1 { get; set; }
        public string BgColor2        { get; set; }
        public string NavUrl { get; set; }

        public List<ModuleFeatureDTO> Items { get; set; }

    }

    public class ModuleFeatureDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuText { get; set; }
        public string MenuIcon { get; set; }
        public string BgColor1 { get; set; }
        public string BgColor2 { get; set; }
        public string NavUrl { get; set; }
    }


}
