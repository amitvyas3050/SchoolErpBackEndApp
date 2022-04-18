/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace Common.Entities
{
    public class UsersGridFilter : BaseFilter
    {
        public string FilterByFirstName { get; set; }
        public string FilterByLastName { get; set; }
        public string FilterByLogin { get; set; }
        public string FilterByEmail { get; set; }
        public string FilterByAge { get; set; }
        public string FilterByStreet { get; set; }
        public string FilterByCity { get; set; }
        public string FilterByZipCode { get; set; }
    }
}