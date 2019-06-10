using LastMinuteTravel.Enums;
using LastMinuteTravel.Settings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace LastMinuteTravel.FunctionalTests
{
    class CruisesSearchTests<T> : TestSetup<T> where T: IWebDriver
    {
        private MainSearchPF mainSearchPF;
        private CruisesSearchPF cruisesSearchPF;

        [Test(Description = "Test cruise search features")]
        //[TestCaseSource(typeof(DriverSetup), "BrowserSelection")]
        public void CruisesSearch()
        {
            mainSearchPF = new MainSearchPF(Driver);
            cruisesSearchPF = new CruisesSearchPF(Driver);

            //string currentMonth = DateTime.Today.AddMonths(1).ToString("MMMM yyyy");
            string currentMonth = DateTime.Today.ToString("MMMM yyyy");
            string[] preSelectedOption = new[] { "CARIBBEAN/BAHAMAS", currentMonth, "Any Length", "Any Cruise Line" };
            string destination = "Eastern Caribbean";
            string month = "August 2019";
            string length = "15+ Nights";
            string cruiseLine = "Disney Cruises";

            try
            {
                Assert.True(mainSearchPF.SelectSearchTab(SearchTab.Cruises), "SelectSearchTab");
                Assert.True(preSelectedOption.SequenceEqual(cruisesSearchPF.ValPreSelectedDropDownListOption()), "ValPreSelectedDropDownListOption");
                Assert.True(cruisesSearchPF.SelectDestination(destination), "SelectDestination");
                Assert.True(cruisesSearchPF.VerifyMonthDropDownListItems(), "VerifyMonthDropDownListItems");
                Assert.True(cruisesSearchPF.SelectMonth(month), "SelectMonth");
                Assert.True(cruisesSearchPF.SelectLength(length), "SelectLength");
                Assert.True(cruisesSearchPF.SelectCruiseLine(cruiseLine), "SelectCruiseLine");
            }
            catch (Exception ex)
            {
                extTest.Fail(ex.Message);
                throw;
            }
        }
    }
}
