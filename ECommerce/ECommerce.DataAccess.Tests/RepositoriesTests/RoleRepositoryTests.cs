/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Services.Infrastructure;
using ECommerce.Entities.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests.RepositoriesTests
{
    [TestClass]
    public class RoleRepository : TestBase<IRoleRepository<Role>, Role>
    {
        private static Role _testRole;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            ClassInitBase();

            _testRole = new Role
            {
                Name = "Test Role"
            };

            await Repository.Edit(_testRole, ContextSession);
        }

        [TestMethod]
        public async Task RoleRepositoryTests()
        {
            await GetRoleByNameTest();
            await GetRoleTest();
            await EditRoleTest();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await Repository.Delete(_testRole.Id, ContextSession);
        }

        #region Test Methods
        
        private async Task GetRoleByNameTest()
        {
            var actual = await Repository.Get(_testRole.Name, ContextSession);

            //improvement: for case if Id will not assigned to test order entity
            _testRole.Id = actual.Id;

            Compare(_testRole, actual, nameof(GetRoleByNameTest));
        }

        private async Task GetRoleTest()
        {
            var actual = await Repository.Get(_testRole.Id, ContextSession);
            Compare(_testRole, actual, nameof(GetRoleTest));
        }

        private async Task EditRoleTest()
        {
            _testRole.Name = "Test Role Edited";

            var actual = await Repository.Edit(_testRole, ContextSession);
            Compare(_testRole, actual, nameof(EditRoleTest));
        }

        #endregion
        
        protected override void Compare(Role actual, Role expected, string message)
        {
            Assert.AreEqual(expected.Id, actual.Id, message);
            Assert.AreEqual(expected.Name, actual.Name, message);
        }
    }
}
