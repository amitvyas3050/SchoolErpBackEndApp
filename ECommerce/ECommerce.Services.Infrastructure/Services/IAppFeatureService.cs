/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities.System;
using ECommerce.DTO;
using ECommerce.Entities;
using System.Threading.Tasks;

namespace ECommerce.Services.Infrastructure
{
    public interface IAppFeatureService
    {
        Task<GridData<AppFeatureDTO>> GetDataForGrid(AppFeatureGridFilter filter);
        Task<AppFeatureDTO> GetById(int id);
        Task<bool> Delete(int id);
        Task<AppFeatureDTO> Edit(AppFeatureDTO dto);
    }
}
