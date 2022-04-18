/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using ECommerce.Entities.System;
using System;

namespace ECommerce.Entities
{
    public class UserActivity : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
