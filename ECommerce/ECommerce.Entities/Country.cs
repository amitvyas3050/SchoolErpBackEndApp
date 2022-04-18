/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using System.Collections.Generic;

namespace ECommerce.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
