/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using ECommerce.Entities;
using ECommerce.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Tests.RepositoriesTests
{
    [TestClass]
    public class CountryRepository : TestBase<ICountryRepository, Country>
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            ClassInitBase();
        }

        [TestMethod]
        public async Task CountryRepositoryTests()
        {
            await GetCountriesListTest();
            await GetCountryTest();
        }

        #region Test Methods

        private async Task GetCountriesListTest()
        {
            var countries = (await Repository.GetList(ContextSession)).ToList();
            
            Assert.IsNotNull(countries, $"{nameof(GetCountriesListTest)} - GetList() should return not null value.");
            Assert.IsTrue(countries.Any(), $"{nameof(GetCountriesListTest)} - GetList() should return not empty list.");
        }

        private async Task GetCountryTest()
        {
            var actual = await Repository.Get(_testCountry1.Id, ContextSession);
            Compare(_testCountry1, actual, nameof(GetCountryTest));
        }

        #endregion

        protected override void Compare(Country actual, Country expected, string message)
        {
            Assert.AreEqual(expected.Id, actual.Id, message);
            Assert.AreEqual(expected.Name, actual.Name, message);
            Assert.AreEqual(expected.Code, actual.Code, message);
        }
    }
}
