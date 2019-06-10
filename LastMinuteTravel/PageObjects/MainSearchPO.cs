using OpenQA.Selenium;
using System.Collections.Generic;

namespace LastMinuteTravel
{
    public class MainSearchPO
    {
        protected IWebDriver driver;

        public MainSearchPO(IWebDriver driver)
        {
            this.driver = driver;
        }

        //General section
        protected IList<IWebElement> TabSearch => driver.FindElements(By.XPath("//*[@role='tab']"));

        protected IList<IWebElement> DdlOption => driver.FindElements(By.CssSelector(".selectBox"));

        protected IWebElement TabHotels => driver.FindElement(By.CssSelector("#react-tabs-0>a"));

        protected IList<IWebElement> TfSearchName => driver.FindElements(By.CssSelector(".autosuggest-input"));

        protected IList<IWebElement> LstAutoSuggestRow => driver.FindElements(By.CssSelector(".loc_item"));

        protected IList<IWebElement> LstAutoSuggest => driver.FindElements(By.CssSelector(".highlight"));

        protected IList<IWebElement> LblCalendarHeader => driver.FindElements(By.XPath("//*[@class='CalendarMonth CalendarMonth--horizontal' and @data-visible='true']/table/caption/strong"));

        protected IWebElement TfCheckInDate => driver.FindElement(By.XPath("//*[@id='checkInDateInput']"));

        protected IWebElement TfCheckOutDate => driver.FindElement(By.XPath("//*[@id='checkOutDateInput']"));

        protected IList<IWebElement> TfBookingDate => driver.FindElements(By.CssSelector(".DateInput__display-text--has-input"));

        protected IWebElement BtnDatePickerPrev => driver.FindElement(By.CssSelector(".DayPickerNavigation__prev.DayPickerNavigation__prev--default"));

        protected IWebElement BtnDatePickerNext => driver.FindElement(By.CssSelector(".DayPickerNavigation__next.DayPickerNavigation__next--default"));

        protected IList<IWebElement> FirstCalValidDay => driver.FindElements(By.XPath("//*[@class='CalendarMonth CalendarMonth--horizontal' and @data-visible='true'][1]/table/tbody/tr/td[contains(@class,'CalendarDay--valid')]"));

        protected IList<IWebElement> LastCalValidDay => driver.FindElements(By.XPath("//*[@class='CalendarMonth CalendarMonth--horizontal' and @data-visible='true'][2]/table/tbody/tr/td[contains(@class,'CalendarDay--valid')]"));

        protected IWebElement TfSelectedOccupant => driver.FindElement(By.CssSelector(".peopleCountTextDesktop"));

        protected IWebElement BtnAddRoom => driver.FindElement(By.CssSelector(".addRoom"));

        protected IList<IWebElement> BtnDeleteRoom => driver.FindElements(By.CssSelector(".cross"));

        protected IList<IWebElement> DdlAdults => driver.FindElements(By.CssSelector(".adultsCol>.selectWrapper>.selectBox"));

        protected IList<IWebElement> DdlChildren => driver.FindElements(By.CssSelector(".childrenCol>.selectWrapper>.selectBox"));

        protected IList<IWebElement> DdlAge => driver.FindElements(By.XPath("//*[@class='childrenList']/li/div/select"));

        protected IWebElement BtnOK => driver.FindElement(By.CssSelector(".okBtn"));

        protected IWebElement BtnCancel => driver.FindElement(By.CssSelector(".occCancelBtn"));

        protected IWebElement DragBox => driver.FindElement(By.CssSelector("#draggable"));

        protected IWebElement DropBox => driver.FindElement(By.CssSelector("#droppable"));

        protected IWebElement BtnFind => driver.FindElement(By.CssSelector("button.btnSubmit"));

        protected IList<IWebElement> RbtnOption => driver.FindElements(By.CssSelector(".css-label"));

        //Flights section
        protected IWebElement TabFlights => driver.FindElement(By.CssSelector("#react-tabs-2>a"));

        protected IList<IWebElement> TfFlyingLocation => driver.FindElements(By.XPath("//*[@id='autosuggest-flightsFrom' or @id='autosuggest-flightsTo']"));

        protected IList<IWebElement> DdlChildrenAge => driver.FindElements(By.XPath("//*[@class='childrenList']/li/div/select"));

        protected IWebElement RbtnRoundTrip => driver.FindElement(By.XPath("//*[@id='radio1']"));

        protected IWebElement RbtnOneWay => driver.FindElement(By.XPath("//*[@id='radio2']"));

        protected IList<IWebElement> RbtnFlightTrip => driver.FindElements(By.CssSelector(".css-label"));

        //Cruises section
        protected IWebElement TabCruises => driver.FindElement(By.CssSelector("#react-tabs-4>a"));

        //Cars section
        protected IWebElement RbtAirport => driver.FindElement(By.XPath("//*[@id='radio3']"));

        protected IWebElement RbtnCity => driver.FindElement(By.XPath("//*[@id='radio4']"));
    }
}
