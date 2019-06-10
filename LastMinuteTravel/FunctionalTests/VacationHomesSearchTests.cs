using LastMinuteTravel.Enums;
using LastMinuteTravel.Settings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace LastMinuteTravel.FunctionalTests
{
    class VacationHomesSearchTests<T> : TestSetup<T> where T : IWebDriver
    {
        private MainSearchPF mainSearchPF;
        private VacationHomesSearchPF vacationHomesSearchPF;

        [Test]
        public void VacationHomesSearch()
        {
            mainSearchPF = new MainSearchPF(Driver);
            vacationHomesSearchPF = new VacationHomesSearchPF(Driver);

            try
            {
                Assert.True(mainSearchPF.SelectSearchTab(SearchTab.VacationHomes), "SelectSearchTab");
                Assert.True(vacationHomesSearchPF.ValPreSelectedDropDownListOption(), "ValPreSelectedDropDownListOption");
                Assert.True(vacationHomesSearchPF.ValChildrenAgeDropDownList(), "ValChildrenAgeDropDownList");
                Assert.True(vacationHomesSearchPF.ValDropDownListOptions(), "ValDropDownListOptions");
            }
            catch (Exception ex)
            {
                extTest.Fail(ex.Message);
                throw;
            }
        }
    }
}
