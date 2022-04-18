/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using ECommerce.Entities.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests.RepositoriesTests
{
    [TestClass]
    public class UserRepository : TestBase<IUserRepository<User>, User>
    {
        private static User _testUser;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            ClassInitBase();

            _testUser = new User
            {
                FirstName = "Test",
                LastName = "User",
                Login = "@testuser",
                Email = "test@user.com",
                Age = 25,
                AddressCity = "Test City",
                AddressStreet = "Test st.",
                AddressZipCode = "TEST123"
            };

            await Repository.Edit(_testUser, ContextSession);
        }

        [TestMethod]
        public async Task UserRepositoryTests()
        {
            await GetFilteredUserTest();
            await GetUserTest();
            await GetByLoginTest();
            await GetUserByEmailTest();
            await EditUserTest();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await Repository.Delete(_testUser.Id, ContextSession);
        }

        #region Test Methods

        private async Task GetFilteredUserTest()
        {
            var filter = new UsersGridFilter
            {
                PageNumber = 1,
                PageSize = 20,
                FilterByFirstName = _testUser.FirstName,
                FilterByLastName = _testUser.LastName,
                FilterByLogin = _testUser.Login,
                FilterByEmail = _testUser.Email,
                FilterByAge = _testUser.Age.ToString(),
                FilterByStreet = _testUser.AddressStreet,
                FilterByCity = _testUser.AddressCity,
                FilterByZipCode = _testUser.AddressZipCode
            };

            var users = await Repository.GetFilteredListWithTotalCount(filter, ContextSession);

            var actual = users.Item1.FirstOrDefault();

            Assert.AreEqual(1, users.Item2);
            Assert.IsNotNull(actual);

            //improvement: for case if Id will not assigned to test order entity
            _testUser.Id = actual.Id;

            Compare(_testUser, actual, nameof(GetFilteredUserTest));
        }

        private async Task GetUserTest()
        {
            var actual = await Repository.Get(_testUser.Id, ContextSession);
            Compare(_testUser, actual, nameof(GetUserTest));
        }

        private async Task GetByLoginTest()
        {
            var actual = await Repository.GetByLogin(_testUser.Login, ContextSession);
            Compare(_testUser, actual, nameof(GetByLoginTest));
        }

        private async Task GetUserByEmailTest()
        {
            var actual = await Repository.GetByEmail(_testUser.Email, ContextSession);
            Compare(_testUser, actual, nameof(GetUserByEmailTest));
        }

        private async Task EditUserTest()
        {
            _testUser.FirstName = "Test Edited";
            _testUser.LastName = "User Edited";
            _testUser.Login = "@testuseredited";
            _testUser.Email = "test@user.edited";
            _testUser.Age = 34;
            _testUser.AddressCity = "Test City Edited";
            _testUser.AddressStreet = "Test st. Edited";
            _testUser.AddressZipCode = "TEST123 Edited";

            await Repository.Edit(_testUser, ContextSession);

            var actual = await Repository.Get(_testUser.Id, ContextSession);
            Compare(_testUser, actual, nameof(EditUserTest));
        }

        #endregion
        
        protected override void Compare(User actual, User expected, string message)
        {
            Assert.AreEqual(expected.Id, actual.Id, message);
            Assert.AreEqual(expected.FirstName, actual.FirstName, message);
            Assert.AreEqual(expected.LastName, actual.LastName, message);
            Assert.AreEqual(expected.Login, actual.Login, message);
            Assert.AreEqual(expected.Email, actual.Email, message);
            Assert.AreEqual(expected.Age, actual.Age, message);
            Assert.AreEqual(expected.AddressStreet, actual.AddressStreet, message);
            Assert.AreEqual(expected.AddressCity, actual.AddressCity, message);
            Assert.AreEqual(expected.AddressZipCode, actual.AddressZipCode, message);
        }
    }
}
