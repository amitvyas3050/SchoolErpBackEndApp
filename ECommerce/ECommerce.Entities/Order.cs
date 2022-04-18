/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities.System;
using System;

namespace ECommerce.Entities
{
    public class Order : TrackableEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
