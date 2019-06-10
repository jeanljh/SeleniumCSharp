using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LastMinuteTravel
{
    class FlightsSearchPF : MainSearchPO
    {
        private int adults;
        private int children;
        private int seniors;
        private int infants;

        public FlightsSearchPF(IWebDriver driver) : base(driver) { }

        public void EnterFlyingFromLocation(string name)
        {
            try
            {
                TabFlights.Click();
                Thread.Sleep(500);
                TfSearchName[0].SendKeys(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public void EnterFlyingToLocation(string name)
        {
            try
            {
                TabFlights.Click();
                Thread.Sleep(500);
                TfSearchName[1].SendKeys(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValFlightAutoSuggestSearch(string name)
        {
            Thread.Sleep(3000);
            try
            {
                for (int row = 1; row <= LstAutoSuggestRow.Count; row++)
                {
                    IList<IWebElement> highlight = driver.FindElements(By.XPath($"//*[@class='react-autosuggest__section-suggestions-container']/li[{row}]/div/span[@class='highlight']"));

                    IEnumerable<string> autoSuggest = highlight.Select(x => x.Text);
                    //IEnumerable<IWebElement> test = highlight.Where(x => x.Text != name).Select(x => x);

                    if (string.Join(" ", autoSuggest) != name)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectFlightOption(string flightOption)
        {
            try
            {
                switch (flightOption.ToLower())
                {
                    case "round trip":
                        RbtnRoundTrip.Click();
                        try
                        {
                            if (TfCheckInDate.Displayed)
                            {
                                //continue if condition is true
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("check in date field does not exist");
                            return false;
                        }
                        try
                        {
                            if (TfCheckOutDate.Displayed)
                            {
                                //continue if condition is true
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("check out date field does not exist");
                            return false;
                        }
                        break;

                    case "one way":
                        RbtnOneWay.Click();
                        try
                        {
                            if (TfCheckInDate.Displayed)
                            {
                                //continue if condition is true
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("check in date field does not exist");
                            return false;
                        }
                        try
                        {
                            if (TfCheckOutDate.Displayed)
                            {
                                Console.WriteLine("check out date field is exist");
                                return false;
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            //continue if condition is true
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public (string, string) ValPreSelectedDropDownListOption()
        {
            string[] selectedOption = new string[10];
            int[] totalOption = new int[10];
            try
            {
                TfSelectedOccupant.Click();
                for (int i = 0; i < 4; i++)
                {
                    selectedOption[i] = new SelectElement(DdlOption[i]).SelectedOption.Text;
                    totalOption[i] = new SelectElement(DdlOption[i]).Options.Count();
                }

                new SelectElement(DdlOption[1]).SelectByValue("6");

                for (int j = 4; j < 10; j++)
                {
                    selectedOption[j] = new SelectElement(DdlOption[j]).SelectedOption.Text;
                    totalOption[j] = new SelectElement(DdlOption[j]).Options.Count();
                }
                //foreach (var item in DdlOption)
                //{
                //    SelectElement selectedValue = new SelectElement(item);

                //    array[item] = selectedValue.SelectedOption.Text;
                //}
                TfSelectedOccupant.Click();
                return (string.Join(", ", selectedOption), string.Join(", ", totalOption));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValChildrenAgeDropDownList()
        {
            try
            {
                TfSelectedOccupant.Click();
                for (int i = 0; i < new SelectElement(DdlOption[1]).Options.Count; i++)
                {
                    new SelectElement(DdlOption[1]).SelectByValue($"{i}");
                    Thread.Sleep(1000);
                    if (DdlOption.Count != i + 5)
                    {
                        Console.WriteLine($"Expected = {DdlOption.Count}, Actual = {i + 5}");
                        return false;
                    }
                }
                TfSelectedOccupant.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValFlightDropDownListState()
        {
            try
            {
                TfSelectedOccupant.Click();
                SelectElement childrenDropDown = new SelectElement(DdlOption[1]);
                childrenDropDown.SelectByValue($"{childrenDropDown.Options.Count - 1}");
                for (int i = 0; i < DdlOption.Count - 1; i++)
                {
                    if (!DdlOption[i].Enabled)
                    {
                        Console.WriteLine($"Drop down list at position {i} is enabled");
                        return false;
                    }
                }
                TfSelectedOccupant.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectPassengers(int adults, int children, int seniors, int infants, int[] age)
        {
            this.adults = adults;
            this.children = children;
            this.seniors = seniors;
            this.infants = infants;

            try
            {
                TfSelectedOccupant.Click();

                SelectElement adultsDropDown = new SelectElement(DdlOption[0]);
                adultsDropDown.SelectByValue(adults.ToString());

                if (DdlOption[0].GetAttribute("value") != adults.ToString())
                {
                    Console.WriteLine($"Expected = {adults}, Actual = {DdlOption[0].GetAttribute("value")}");
                    return false;
                }

                SelectElement childrenDropDown = new SelectElement(DdlOption[1]);
                childrenDropDown.SelectByValue(children.ToString());

                if (DdlOption[1].GetAttribute("value") != children.ToString())
                {
                    Console.WriteLine($"Expected = {children}, Actual = {DdlOption[1].GetAttribute("value")}");
                    return false;
                }

                SelectElement seniorsDropDown = new SelectElement(DdlOption[2]);
                seniorsDropDown.SelectByValue(seniors.ToString());

                if (DdlOption[2].GetAttribute("value") != seniors.ToString())
                {
                    Console.WriteLine($"Expected = {seniors}, Actual = {DdlOption[2].GetAttribute("value")}");
                    return false;
                }

                SelectElement infantsDropDown = new SelectElement(DdlOption[3]);
                infantsDropDown.SelectByValue(infants.ToString());

                if (DdlOption[3].GetAttribute("value") != infants.ToString())
                {
                    Console.WriteLine($"Expected = {seniors}, Actual = {DdlOption[3].GetAttribute("value")}");
                    return false;
                }

                for (int i = 0; i < age.Length; i++)
                {
                    SelectElement ageDropDown = new SelectElement(DdlOption[4 + i]);
                    ageDropDown.SelectByValue(age[i].ToString());
                    if (DdlOption[4 + i].GetAttribute("value") != age[i].ToString())
                    {
                        Console.WriteLine($"Expected = {age[i]}, Actual = {DdlOption[4 + i].GetAttribute("value")}");
                        return false;
                    }
                }
                TfSelectedOccupant.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValTotalSelectedPassengers()
        {
            try
            {
                string totalPassengers = $"{adults + children + seniors + infants} Passengers";
                return TfSelectedOccupant.Text == totalPassengers ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} Error: {ex.Message}");
                throw;
            }
        }

        public bool SelectFlightClass(string flightClass)
        {
            string[] flightClassExpectedSelections = { "Economy", "Business", "First Class" };
            try
            {
                SelectElement selectFlightClass = new SelectElement(DdlOption.Last());
                if (selectFlightClass.SelectedOption.Text != "Economy")
                {
                    Console.WriteLine($"Default Selected Option: Expected = Economy, Actual = {selectFlightClass.SelectedOption.Text}");
                    return false;
                }

                List<string> flightClassActualSelections = new List<string>();
                for (int i = 0; i < selectFlightClass.Options.Count; i++)
                {
                    selectFlightClass.SelectByIndex(i);
                    flightClassActualSelections.Add(selectFlightClass.SelectedOption.Text);
                }
                if (!flightClassExpectedSelections.SequenceEqual(flightClassActualSelections))
                {
                    Console.WriteLine($"Available Selections:" +
                        $"\nExpected = {string.Join(", ", flightClassExpectedSelections)}" +
                        $"\nActual = {string.Join(", ", flightClassActualSelections)}");
                    return false;
                }

                selectFlightClass.SelectByText(flightClass);
                if (selectFlightClass.SelectedOption.Text != flightClass)
                {
                    Console.WriteLine($"User Selected Option: Expected = {flightClass}, Actual = {selectFlightClass.SelectedOption.Text}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }
    }
}
