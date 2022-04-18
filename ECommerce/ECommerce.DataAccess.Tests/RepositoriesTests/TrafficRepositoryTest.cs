/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests.RepositoriesTests
{
    [TestClass]
    public class TrafficRepository : TestBase<ITrafficRepository, Traffic>
    {
        private static Traffic _testTrafficEntity;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            ClassInitBase();

            _testTrafficEntity = new Traffic
            {
                Date = DateTime.Now.AddYears(-2),
                Value = 24
            };

            await Repository.Edit(_testTrafficEntity, ContextSession);
        }

        [TestMethod]
        public async Task TrafficRepositoryTest()
        {
            await GetTrafficEntityTest();
            await EditTrafficEntityTest();
        }

        #region Test Methods
        
        private async Task GetTrafficEntityTest()
        {
            var actual = await Repository.Get(_testTrafficEntity.Id, ContextSession);
            Compare(_testTrafficEntity, actual, nameof(GetTrafficEntityTest));
        }

        private async Task EditTrafficEntityTest()
        {
            _testTrafficEntity.Date = DateTime.Now;
            _testTrafficEntity.Value = 22;

            await Repository.Edit(_testTrafficEntity, ContextSession);

            var actual = await Repository.Get(_testTrafficEntity.Id, ContextSession);
            Compare(_testTrafficEntity, actual, nameof(EditTrafficEntityTest));
        }

        #endregion

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await Repository.Delete(_testTrafficEntity.Id, ContextSession);
        }

        protected override void Compare(Traffic actual, Traffic expected, string message)
        {
            Assert.AreEqual(expected.Id, actual.Id, message);
            Assert.AreEqual(expected.Value, actual.Value, message);
            Assert.AreEqual(expected.Date.Date, actual.Date.Date, message);
        }
    }
}
