/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using System;

namespace ECommerce.Entities
{
    public class ListFilter : BaseFilter
    {
        public ListFilter(int pageNumber = 1, int pageSize = 10, DateTime? startDate = null, DateTime? endDate = null) : base(pageNumber, pageSize)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
