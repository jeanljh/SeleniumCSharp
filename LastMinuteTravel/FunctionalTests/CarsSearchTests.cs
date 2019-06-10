using LastMinuteTravel.Enums;
using LastMinuteTravel.PageFunctions;
using LastMinuteTravel.Settings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace LastMinuteTravel.FunctionalTests
{
    class CarsSearchTests<T> : TestSetup<T> where T : IWebDriver
    {
        private MainSearchPF mainSearchPF;
        private CarsSearchPF carsSearchPF;

        [Test]
        public void VerifyCarsSearchFeatures()
        {
            mainSearchPF = new MainSearchPF(Driver);
            carsSearchPF = new CarsSearchPF(Driver);

            DateTime bookingDate = new DateTime(2019, 7, 1);
            string pickup = "Kuala Lumpur";
            string dropOff = "Kuala Lumpur";

            try
            {
                Assert.True(mainSearchPF.SelectSearchTab(SearchTab.Cars), "SelectSearchTab", mainSearchPF.ErrMsg);
                Assert.True(mainSearchPF.VerifyAutoSuggestSearch(pickup), "VerifyAutoSuggestSearch 1");
                Assert.True(mainSearchPF.VerifyAutoSuggestSearch(dropOff, 1), "VerifyAutoSuggestSearch 2");
                Assert.True(carsSearchPF.SelectBookingDate(bookingDate));
                Assert.True(carsSearchPF.CheckPreSelectedTime(), "CheckPreSelectedTime");
                Assert.True(carsSearchPF.CheckDropDownListItems(), "CheckDropDownListItems");
                Assert.True(carsSearchPF.CheckTotalDropDownListItems(), "CheckTotalDropDownListItems");
                Assert.True(carsSearchPF.SelectTimeDropDownList(1, "7:00"), "SelectTimeDropDownList");
                Assert.True(carsSearchPF.VerifySearchCarNearOption(), "VerifySearchCarNearOption");
            }
            catch (Exception ex)
            {
                extTest.Fail(ex.Message);
                throw;
            }
        }
    }
}
