using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Reflection;

namespace LastMinuteTravel
{
    class VacationHomesSearchPF : MainSearchPO
    {
        public VacationHomesSearchPF(IWebDriver driver) : base(driver) { }

        public bool ValPreSelectedDropDownListOption()
        {
            int totalOption;
            try
            {
                TfSelectedOccupant.Click();

                string selectedOption = new SelectElement(DdlOption[0]).SelectedOption.Text;
                if (selectedOption != "1 adult")
                {
                    Console.WriteLine($"Expected = 1 adult\nActual = {selectedOption}");
                    return false;
                }

                selectedOption = new SelectElement(DdlOption[1]).SelectedOption.Text;
                if (selectedOption != "0 children")
                {
                    Console.WriteLine($"Expected = 0 children\nActual = {selectedOption}");
                    return false;
                }

                if (DdlOption.Count != 2)
                {
                    Console.WriteLine($"Expected = 2\nActual = {DdlOption.Count}");
                    return false;
                }

                for (int i = 0; i < DdlOption.Count; i++)
                {
                    totalOption = new SelectElement(DdlOption[i]).Options.Count;

                    if (totalOption != 21)
                    {
                        Console.WriteLine($"Drop down list number {i + 1}:" +
                            "\nExpected = 21" +
                            $"\nActual = {totalOption}");
                        return false;
                    }
                }

                new SelectElement(DdlOption[1]).SelectByValue("20");

                if (DdlOption.Count != 22)
                {
                    Console.WriteLine($"Expected = 22\nActual = {DdlOption.Count}");
                    return false;
                }

                for (int i = 2; i < DdlOption.Count; i++)
                {
                    selectedOption = new SelectElement(DdlOption[i]).SelectedOption.Text;
                    totalOption = new SelectElement(DdlOption[i]).Options.Count;

                    if (selectedOption != "12")
                    {
                        Console.WriteLine($"Drop down list number {i + 1}:" +
                            "\nExpected = 12" +
                            $"\nActual = {selectedOption}");
                        return false;
                    }
                    if (totalOption != 17)
                    {
                        Console.WriteLine($"Drop down list number {i + 1}:" +
                            "\nExpected = 17" +
                            $"\nActual = {totalOption}");
                        return false;
                    }
                }
                BtnOK.Click();
                return true;
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
                    new SelectElement(DdlOption[1]).SelectByValue(i.ToString());
                    try
                    {
                        if (DdlOption[i + 1].Displayed)
                        {
                            //Continue if true
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine($"Drop down list number {i + 2} does not exist");
                        return false;
                    }
                    if (DdlOption.Count != 2 + i)
                    {
                        Console.WriteLine($"Expected = {2 + i}" +
                            $"\nActual = {DdlOption.Count}");
                        return false;
                    }
                }
                BtnOK.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValDropDownListOptions()
        {
            string optionName;
            string[] expectedAdultsOptions = new string[21];
            string[] expectedChildrenOptions = new string[21];
            string[] expectedChildrenAgeOptions = new string[17];

            for (int i = 0; i < expectedAdultsOptions.Length; i++)
            {
                if (i == 1)
                {
                    optionName = "adult";
                }
                else
                {
                    optionName = "adults";
                }
                expectedAdultsOptions[i] = $"{i} {optionName}";
            }

            for (int i = 0; i < expectedChildrenOptions.Length; i++)
            {
                if (i == 1)
                {
                    optionName = "child";
                }
                else
                {
                    optionName = "children";
                }
                expectedChildrenOptions[i] = $"{i} {optionName}";
            }

            for (int i = 0; i < expectedChildrenAgeOptions.Length; i++)
            {
                expectedChildrenAgeOptions[i] = $"{i + 1}";
            }

            try
            {
                TfSelectedOccupant.Click();
                if (!new SelectElement(DdlOption[0]).Options.Select(x => x.Text).SequenceEqual(expectedAdultsOptions))
                {
                    Console.WriteLine($"Adults drop down list options are incorrect");
                    return false;
                }

                if (!new SelectElement(DdlOption[1]).Options.Select(x => x.Text).SequenceEqual(expectedChildrenOptions))
                {
                    Console.WriteLine("Children drop down list options are incorrect");
                    return false;
                }

                for (int i = 2; i < DdlOption.Count; i++)
                {
                    if (!new SelectElement(DdlOption[i]).Options.Select(x => x.Text).SequenceEqual(expectedChildrenAgeOptions))
                    {
                        Console.WriteLine($"Children age drop down list number {i - 1} options are incorrect");
                        return false;
                    }
                }
                BtnOK.Click();
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