using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LastMinuteTravel.PageFunctions
{
    class CarsSearchPF : MainSearchPO
    {
        public CarsSearchPF(IWebDriver driver) : base(driver) { }

        public bool SelectBookingDate(DateTime date)
        {
            bool dateMatch = false;
            bool isMatch = false;

            try
            {
                TfCheckInDate.Click();

                do
                {
                    DateTime.TryParseExact(date.ToString("MMMM yyyy"), "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime selectDate);
                    DateTime.TryParseExact(LblCalendarHeader.First().Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime firstHeaderDate);
                    DateTime.TryParseExact(LblCalendarHeader.Last().Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime lastHearderDate);

                    if (selectDate < firstHeaderDate)
                    {
                        BtnDatePickerPrev.Click();
                        Thread.Sleep(500);
                    }
                    else if (selectDate > lastHearderDate)
                    {
                        BtnDatePickerNext.Click();
                        Thread.Sleep(500);
                    }
                    else
                    {
                        if (selectDate == firstHeaderDate)
                        {
                            foreach (var item in FirstCalValidDay)
                            {
                                if (item.Text == date.Day.ToString())
                                {
                                    dateMatch = true;
                                    item.Click();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in LastCalValidDay)
                            {
                                if (item.Text == date.Day.ToString())
                                {
                                    dateMatch = true;
                                    item.Click();
                                    break;
                                }
                            }
                        }
                        if (dateMatch == false)
                        {
                            Console.WriteLine("Entered date is not current valid date");
                            return false;
                        }
                        isMatch = true;
                    }
                } while (isMatch == false);

                if (TfBookingDate.First().Text.ToLower() != date.ToString("MMM dd").ToLower())
                {
                    Console.WriteLine($"Expected = {date.ToString("MMM dd").ToLower()}" +
                        $"\nActual = {TfBookingDate.First().Text.ToLower()}");
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

        public bool SelectTimeDropDownList(int index, string hour)
        {
            try
            {
                SelectElement element = new SelectElement(DdlOption[index]);
                element.SelectByText(hour);
                if (element.SelectedOption.Text != hour)
                {
                    Console.WriteLine($"Expected = {hour}" +
                        $"\nActual = {element.SelectedOption.Text}");
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

        public bool CheckPreSelectedTime()
        {
            try
            {
                foreach (var item in DdlOption)
                {
                    if (new SelectElement(item).SelectedOption.Text != "10:00")
                    {
                        Console.WriteLine("Expected = 10:00" +
                            $"\nActual = {new SelectElement(item).SelectedOption.Text}");
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

        public bool CheckTotalDropDownListItems()
        {
            try
            {
                foreach (var item in DdlOption)
                {
                    if (new SelectElement(item).Options.Count != 24)
                    {
                        Console.WriteLine("Expected = 24" +
                            $"\nActual = {new SelectElement(item).Options.Count}");
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

        public bool CheckDropDownListItems()
        {
            string[] expectedOptions = new string[24];
            for (int i = 0; i < 24; i++)
            {
                expectedOptions[i] = $"{i}:00";
            }

            try
            {
                foreach (var item in DdlOption)
                {
                    IEnumerable<string> actualOptions = new SelectElement(item).Options.Select(x => x.Text);
                    if (!actualOptions.SequenceEqual(expectedOptions))
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

        public bool VerifySearchCarNearOption()
        {
            try
            {
                RbtnOption[1].Click();
                if (TfSearchName.Count != 1)
                {
                    Console.WriteLine("Expected = 1" +
                        $"\nActual = {TfSearchName.Count}");
                    return false;
                }
                if (TfSearchName[0].GetAttribute("placeholder") != "Pick Up / Drop Off")
                {
                    Console.WriteLine("Expected = Pick Up / Drop Off" +
                        $"\nActual = {TfSearchName[0].GetAttribute("placeholder")}");
                    return false;
                }

                RbtnOption[0].Click();

                if (TfSearchName.Count != 2)
                {
                    Console.WriteLine("Expected = 2" +
                        $"\nActual = {TfSearchName.Count}");
                    return false;
                }
                if (TfSearchName[0].GetAttribute("placeholder") != "Pick Up")
                {
                    Console.WriteLine("Expected = Pick Up" +
                        $"\nActual = {TfSearchName[0].GetAttribute("placeholder")}");
                    return false;
                }

                if (TfSearchName[1].GetAttribute("placeholder") != "Drop Off")
                {
                    Console.WriteLine("Expected = Drop Off" +
                        $"\nActual = {TfSearchName[1].GetAttribute("placeholder")}");
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
