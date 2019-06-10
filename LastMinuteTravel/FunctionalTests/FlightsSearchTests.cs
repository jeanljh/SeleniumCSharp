using LastMinuteTravel.Settings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace LastMinuteTravel.FunctionalTests
{
    class FlightsSearchTests<T> : TestSetup<T> where T : IWebDriver
    {
        private MainSearchPF mainSearchPF;
        private FlightsSearchPF flightsSearchPF;

        [Test]
        public void FlightsSearch()
        {
            mainSearchPF = new MainSearchPF(Driver);
            flightsSearchPF = new FlightsSearchPF(Driver);

            string fromName = "Kuala Lumpur";
            string toName = "Gold Coast";
            DateTime fromDate = new DateTime(2019, 7, 1);
            DateTime toDate = new DateTime(2019, 8, 31);
            int adults = 1;
            int children = 2;
            int seniors = 3;
            int infants = 4;
            int[] age = { 2, 17 };
            string flightClass = "Business";

            flightsSearchPF.EnterFlyingFromLocation(fromName);
            Assert.True(flightsSearchPF.ValFlightAutoSuggestSearch(fromName), "FlightsSearch");
            flightsSearchPF.EnterFlyingToLocation(toName);
            Assert.True(flightsSearchPF.ValFlightAutoSuggestSearch(toName), "FlightsSearch");
            Assert.True(mainSearchPF.SelectBookingDate(fromDate, toDate), "SelectBookingDate");
            Assert.AreEqual("1 Passenger", mainSearchPF.GetPreSelectedOccupants(), "GetPreSelectedOccupants");
            var (selectedOption, totalOption) = flightsSearchPF.ValPreSelectedDropDownListOption();
            Assert.AreEqual("1 Adult, 0 Children, 0 Seniors, 0 Infants, 12, 12, 12, 12, 12, 12", selectedOption, "ValPreSelectedDropDownListOption");
            Assert.AreEqual("7, 7, 7, 7, 16, 16, 16, 16, 16, 16", totalOption, "ValPreSelectedDropDownListOption");
            Assert.True(flightsSearchPF.ValChildrenAgeDropDownList(), "ValChildrenAgeDropDownList");
            Assert.True(flightsSearchPF.ValFlightDropDownListState(), "ValFlightDropDownListState");
            Assert.True(flightsSearchPF.SelectPassengers(adults, children, seniors, infants, age), "SelectTotalPassengers");
            Assert.True(flightsSearchPF.ValTotalSelectedPassengers());
            Assert.True(flightsSearchPF.SelectFlightClass(flightClass));
        }
    }
}
