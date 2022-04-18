/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.Entities;
using Common.Services.Infrastructure;

namespace ECommerce.DataAccess.EFCore
{
    public class UserPhotoRepository : BaseRepository<UserPhoto, ECommerceDataContext>, IUserPhotoRepository
    {
        public UserPhotoRepository(ECommerceDataContext context) : base(context)
        {
        }
    }
}