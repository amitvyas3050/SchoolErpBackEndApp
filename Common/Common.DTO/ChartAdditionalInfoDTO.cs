/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace Common.DTO
{
    public class ChartAdditionalInfoDTO<T>
    {
        public string Title { get; set; }
        public T Value { get; set; }
    }
}
