using ECommerce.Entities.System;
using System;
using System.Collections.Generic;

namespace ECommerce.Entities
{
    public class AppModule : TrackableEntity
    {
        public string Name { get; set; }
        public string MenuText { get; set; }
        public string MenuIcon { get; set; }
        public string BgColor1 { get; set; }
        public string BgColor2 { get; set; }
        public virtual List<AppFeature> Features { get; set; }

    }
}
