using LastMinuteTravel.Enums;
using LastMinuteTravel.PageFunctions;
using LastMinuteTravel.Settings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace LastMinuteTravel.FunctionalTests
{
    class PackagesSearchTests<T> : TestSetup<T> where T : IWebDriver
    {
        private MainSearchPF mainSearchPF;
        private PackagesSearchPF packagesSearchPF;

        [Test]
        public void PackagesSearch()
        {
            mainSearchPF = new MainSearchPF(Driver);
            packagesSearchPF = new PackagesSearchPF(Driver);

            int room = 3;
            int[] adults = { 1, 2, 3 };
            int[] children = { 0, 1, 2 };
            int[] seniors = { 4, 3, 2 };
            int[] infants = { 0, 1, 2 };
            List<int[]> age = new List<int[]>() { Array.Empty<int>(), new[] { 2 }, new[] { 3, 4 } };
            //List<int[]> age = new List<int[]>() { Array.Empty<int>(), Array.Empty<int>() };

            try
            {
                Assert.True(mainSearchPF.SelectSearchTab(SearchTab.Packages), "SelectSearchTab");
                Assert.True(packagesSearchPF.ValPreSelectedDropDownListOption(), "ValPreSelectedDropDownListOption()");
                Assert.True(packagesSearchPF.ValDropDownListOptionCount(), "ValDropDownListOptionsCount");
                Assert.True(packagesSearchPF.ValDropDownListOptionValue(), "ValDropDownListOptionValue");
                Assert.True(packagesSearchPF.ValCancelRoomAddition(), "ValCancelRoomAddition");
                Assert.True(packagesSearchPF.SelectRoomAndGuest(room, adults, children, seniors, infants, age), "SelectRoomAndGuest");
            }
            catch (Exception ex)
            {
                extTest.Fail(ex.Message);
                throw;
            }
        }
    }
}
