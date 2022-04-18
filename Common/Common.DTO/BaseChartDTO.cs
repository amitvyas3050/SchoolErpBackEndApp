/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;

namespace Common.DTO
{
    public class BaseChartDTO<T> where T : struct
    {
        public IList<ChartDataDTO<T>> Lines { get; set; }
        public IEnumerable<string> AxisXLabels { get; set; }
        public string ChartLabel { get; set; }
    }
}
