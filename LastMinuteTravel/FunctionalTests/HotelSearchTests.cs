using LastMinuteTravel.Enums;
using LastMinuteTravel.Settings;
using LastMinuteTravel.Util;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LastMinuteTravel.FunctionalTests
{
    class HotelsSearchTests<T> : TestSetup<T> where T : IWebDriver, new()
    {
        private MainSearchPF mainSearchPF;

        [Test]
        [Order(1)]
        public void VerifyHotelSearchFeatures()
        {
            mainSearchPF = new MainSearchPF(Driver);

            string name = "cyberjaya";
            DateTime startDate = DateTime.Today.AddDays(UtilFunctions<T>.GenerateRandonNumber(120));
            DateTime endDate = startDate.AddDays(UtilFunctions<T>.GenerateRandonNumber(32));
            int room = 3;
            int[] adults = { 1, 2, 3 };
            int[] children = new[] { 3, 2, 1 };
            List<int[]> age = new List<int[]>() { new[] { 17, 16, 15 }, new[] { 14, 13 }, new[] { 12 } };
            //List<int[]> age = new List<int[]>() { new[] { 0 }, new[] { 1, 2 }, new[] { 1, 2, 3 } };

            Assert.True(mainSearchPF.SelectSearchTab(SearchTab.Hotels), "SelectSearchTab");
            Assert.AreEqual(true, mainSearchPF.VerifyAutoSuggestSearch(name));
            //Assert.True(hotelSearchPF.VerifyHotelAutoSuggestSearch(name));
            mainSearchPF.EnterSearchName(name);
            //hotelSearchPF.SelectBookingDate(fromDate, fromDateType);
            Assert.True(mainSearchPF.SelectBookingDate(startDate, endDate), "SelectBookingDate");
            Thread.Sleep(10000);

            Assert.AreEqual("1 Room, 2 Guests", mainSearchPF.GetPreSelectedOccupants(), "GetPreSelectedOccupants");
            Assert.True(mainSearchPF.SelectTotalOccupants(room, adults, children, age), "SelectTotalOccupants");
            Assert.True(mainSearchPF.GetPostSelectedOccupants(), "GetPostSelectedOccupants");
        }

        [Test]
        [Order(2)]
        public void VerifyHotelSearch()
        {
            mainSearchPF = new MainSearchPF(Driver);

            string name = "cyberjaya";
            DateTime fromDate = new DateTime(2019, 7, 7);
            DateTime toDate = new DateTime(2019, 8, 1);

            int room = 3;
            int[] adults = { 1, 2, 3 };
            int[] children = { 1, 2, 3 };
            List<int[]> age = new List<int[]>() { new[] { 0 }, new[] { 1, 2 }, new[] { 1, 2, 3 } };

            Assert.True(mainSearchPF.SelectSearchTab(SearchTab.Hotels), "SelectSearchTab");
            mainSearchPF.EnterSearchName(name);
            Assert.True(mainSearchPF.SelectBookingDate(fromDate, toDate), "SelectBookingDate");
            Assert.True(mainSearchPF.SelectTotalOccupants(room, adults, children, age), "SelectTotalOccupants");
            mainSearchPF.Search(30);
            //Utility<TWebDriver>.WaitUntilElementVisible(30, By.CssSelector(".fullLoaderSpinner"));
            //Assert.True(Utility<TWebDriver>.WaitUntilElementInvisible(30, By.CssSelector(".fullLoaderSpinner")), "WaitUntilElementInvisible");
        }
    }
}
