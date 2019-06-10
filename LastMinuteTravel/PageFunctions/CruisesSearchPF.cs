using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LastMinuteTravel
{
    class CruisesSearchPF : MainSearchPO
    {
        public CruisesSearchPF(IWebDriver driver) : base(driver) { }


        public List<string> ValPreSelectedDropDownListOption()
        {
            List<string> actualSelectedOption = new List<string>();

            try
            {
                foreach (var item in DdlOption)
                {
                    actualSelectedOption.Add(new SelectElement(item).SelectedOption.Text);
                }
                return actualSelectedOption;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectDestination(string destination)
        {
            try
            {
                SelectElement selectDestination = new SelectElement(DdlOption[0]);
                selectDestination.SelectByText(destination, true);

                string selectedDestination = selectDestination.SelectedOption.Text.TrimStart();
                if (selectedDestination == destination)
                {
                    return true;
                }
                Console.WriteLine($"Expected = {destination}\nActual = {selectedDestination}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectMonth(string month)
        {
            try
            {
                SelectElement selectMonth = new SelectElement(DdlOption[1]);
                selectMonth.SelectByText(month);

                string selectedMonth = selectMonth.SelectedOption.Text;
                if (selectedMonth == month)
                {
                    return true;
                }
                Console.WriteLine($"Expected = {month}\nActual = {selectedMonth}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectLength(string length)
        {
            try
            {
                SelectElement selectLength = new SelectElement(DdlOption[2]);
                string selectedLength = selectLength.SelectedOption.Text;

                if (selectedLength != "Any Length")
                {
                    Console.WriteLine($"Pre-selected length\nExpected = Any Length\nActual = {selectedLength}");
                    return false;
                }

                selectLength.SelectByText(length);
                selectedLength = selectLength.SelectedOption.Text;

                if (selectedLength != length)
                {
                    Console.WriteLine($"Post-selected length\nExpected = {length}\nActual = {selectedLength}");
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

        public bool SelectCruiseLine(string cruiseLine)
        {
            try
            {
                SelectElement selectCruiseLine = new SelectElement(DdlOption[3]);
                string selectedCruiseLine = selectCruiseLine.SelectedOption.Text;

                if (selectedCruiseLine != "Any Cruise Line")
                {
                    Console.WriteLine("Pre-selected cruise line" +
                        "\nExpected = Any Cruise Line" +
                        $"\nActual = {selectedCruiseLine}");
                    return false;
                }

                selectCruiseLine.SelectByText(cruiseLine);
                selectedCruiseLine = selectCruiseLine.SelectedOption.Text;

                if (selectedCruiseLine != cruiseLine)
                {
                    Console.WriteLine("Post-selected cruise line" +
                        $"\nExpected = {cruiseLine}" +
                        $"\nActual = {selectedCruiseLine}");
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

        public bool VerifyMonthDropDownListItems()
        {
            try
            {
                SelectElement monthList = new SelectElement(DdlOption[1]);
                IEnumerable<string> actualList = monthList.Options.Select(x => x.Text);
                string[] expectedList = new string[monthList.Options.Count];
                for (int i = 0; i < monthList.Options.Count; i++)
                {
                    expectedList[i] = DateTime.Today.AddMonths(i).ToString("MMMM yyyy");
                }
                if (!expectedList.SequenceEqual(actualList))
                {
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
