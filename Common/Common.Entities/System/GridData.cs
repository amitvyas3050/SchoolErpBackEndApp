/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;

namespace Common.Entities.System
{
    public class GridData<TDto> where TDto: class, new()
    {
        public int TotalCount { get; set; }
        public IEnumerable<TDto> Items { get; set; }
    }
}