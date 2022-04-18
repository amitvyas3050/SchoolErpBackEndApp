/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities.System;
using System;

namespace ECommerce.Entities
{
    public class Class : TrackableEntity
    {
        public string ClassName { get; set; }
        public string Description { get; set; }
       
    }
}
