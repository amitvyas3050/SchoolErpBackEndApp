/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace ECommerce.Entities
{
    public class OrdersSummary
    {
        public int LastMonth { get; set; }
        public int LastWeek { get; set; }
        public int Today { get; set; }
        public int Marketplace { get; set; }
    }
}
