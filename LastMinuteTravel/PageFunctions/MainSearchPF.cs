using LastMinuteTravel.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LastMinuteTravel
{
    class MainSearchPF : MainSearchPO
    {
        public MainSearchPF(IWebDriver driver) : base(driver) { }

        public string ErrMsg { get; set; }
        private int room;
        private int[] adults;
        private int[] children;

        public bool SelectSearchTab(SearchTab searchtab)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");

            int index = 0;
            try
            {
                switch (searchtab)
                {
                    case SearchTab.Hotels:
                        TabSearch[0].Click();
                        index = 0;
                        break;
                    case SearchTab.Flights:
                        TabSearch[1].Click();
                        index = 1;
                        break;
                    case SearchTab.Cruises:
                        TabSearch[2].Click();
                        index = 2;
                        break;
                    case SearchTab.Cars:
                        TabSearch[3].Click();
                        index = 3;
                        break;
                    case SearchTab.VacationHomes:
                        TabSearch[4].Click();
                        index = 4;
                        break;
                    case SearchTab.Packages:
                        TabSearch[5].Click();
                        index = 5;
                        break;
                    case SearchTab.Activities:
                        TabSearch[6].Click();
                        index = 6;
                        break;
                }
                Thread.Sleep(500);
                if (TabSearch[index].GetAttribute("aria-selected") == "true")
                {
                    return true;
                }

                ErrMsg = "Selected tab is not matched with input tab";
                Console.WriteLine(ErrMsg);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public void EnterSearchName(string name, int index = 0)
        {
            try
            {
                TfSearchName[index].Click();

                if (TfSearchName[index].GetAttribute("value") != string.Empty)
                {
                    TfSearchName[index].Clear();
                }
                TfSearchName[index].SendKeys(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
            try
            {
                LstAutoSuggest[0].Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No search result from auto suggest");
                throw;
            }
        }

        public bool VerifyAutoSuggestSearch(string name, int index = 0)
        {
            try
            {
                EnterSearchName(name, index);
                Thread.Sleep(2000);

                for (int i = 0; i < LstAutoSuggest.Count; i++)
                {
                    if (!LstAutoSuggest[i].Text.ToLower().Contains(name.ToLower()))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error:\n{ex.Message}");
                throw;
                //throw new Exception($"VerifyAutoSuggestSearch Error:\n{ex.Message}");
            }
        }

        public void SelectBookingDate(DateTime date, string dateType)
        {
            bool isMatch = false;
            bool dateMatch = false;
            try
            {
                switch (dateType.ToLower())
                {
                    case "fromdate":
                        TfCheckInDate.Click();
                        break;
                    case "todate":
                        TfCheckOutDate.Click();
                        break;
                }

                do
                {
                    DateTime.TryParseExact(date.ToString("MMMM yyyy"), "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime selectDate);
                    DateTime.TryParseExact(LblCalendarHeader.First().Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime firstHeaderDate);
                    DateTime.TryParseExact(LblCalendarHeader.Last().Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime lastHeaderDate);
                    if (selectDate < firstHeaderDate)
                    {
                        BtnDatePickerPrev.Click();
                    }
                    else if (selectDate > lastHeaderDate)
                    {
                        BtnDatePickerNext.Click();
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
                                }
                            }
                        }

                        if (dateMatch == false)
                        {
                            throw new Exception("Entered date is not current valid date");
                        }
                        isMatch = true;
                    }
                } while (isMatch == false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectBookingDate(params DateTime[] date)
        {
            try
            {
                TfCheckInDate.Click();

                for (int i = 0; i < date.Length; i++)
                {
                    bool isMatch = false;
                    bool dateMatch = false;

                    do
                    {
                        DateTime.TryParseExact(date[i].ToString("MMMM yyyy"), "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime selectDate);
                        DateTime.TryParseExact(LblCalendarHeader.First().Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime firstHeaderDate);
                        DateTime.TryParseExact(LblCalendarHeader.Last().Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime lastHeaderDate);

                        //var headerDate = LblCalendarHeader.Select(x => DateTime.ParseExact(x.Text, "MMMM yyyy", CultureInfo.InvariantCulture)).ToArray();
                        //var headerDate = LblCalendarHeader.Select(x => DateTime.TryParseExact(x.Text, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime y)).ToArray();
                        //var headerDate3 = DateTime.TryParseExact(LblCalendarHeader.Select(x => x.Text).ToString(), "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime y);

                        if (selectDate < firstHeaderDate)
                        {
                            BtnDatePickerPrev.Click();
                            Thread.Sleep(500);
                        }
                        else if (selectDate > lastHeaderDate)
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
                                    if (item.Text == date[i].Day.ToString())
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
                                    if (item.Text == date[i].Day.ToString())
                                    {
                                        dateMatch = true;
                                        item.Click();
                                        break;
                                    }
                                }
                            }

                            if (dateMatch == false)
                            {
                                //throw new Exception("Entered date is not current valid date");
                                Console.WriteLine("Entered date is not current valid date");
                                return false;
                            }
                            isMatch = true;
                        }
                    } while (isMatch == false);

                    if (date[i] == date[0] && TfBookingDate.First().Text.ToLower() != date[0].ToString("MMM dd").ToLower())
                    {
                        Console.WriteLine("Diplayed from date does not match with selected from date");
                        return false;
                    }
                    else if (date[i] == date[1] && TfBookingDate.Last().Text.ToLower() != date[1].ToString("MMM dd").ToLower())
                    {
                        Console.WriteLine("Diplayed to date does not match with selected to date");
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

        public string GetPreSelectedOccupants()
        {
            try
            {
                return TfSelectedOccupant.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectTotalOccupants(int room, int[] adults, int[] children, List<int[]> age)
        {
            this.room = room;
            this.adults = adults;
            this.children = children;

            int count = 0;
            try
            {
                TfSelectedOccupant.Click();
                for (int i = 0; i < room; i++)
                {
                    if (i > 0)
                    {
                        BtnAddRoom.Click();
                    }

                    SelectElement selectAdults = new SelectElement(DdlAdults[i]);
                    selectAdults.SelectByValue(adults[i].ToString());
                    if (selectAdults.SelectedOption.Text.Split(' ').First() != adults[i].ToString())
                    //var text = selectAdults.SelectedOption.Text.IndexOf(' ');
                    //if (selectAdults.SelectedOption.Text.Substring(0, text) != adults[i].ToString())
                    {
                        Console.WriteLine("Selected adult value does not match with entered adult value");
                        return false;
                    }

                    SelectElement selectChildren = new SelectElement(DdlChildren[i]);
                    selectChildren.SelectByValue(children[i].ToString());
                    if (selectChildren.SelectedOption.Text.Split(' ').First() != children[i].ToString())
                    {
                        Console.WriteLine("Selected children value does not match with entered children value");
                        return false;
                    }

                    for (int j = 0; j < children[i]; j++)
                    {
                        SelectElement selectAge = new SelectElement(DdlAge[j + count]);
                        //selectAge.SelectByValue(age[i,j].ToString());
                        selectAge.SelectByValue(age[i][j].ToString());
                        //if (selectAge.SelectedOption.Text != age[i, j].ToString())
                        if (selectAge.SelectedOption.Text.Split(' ').First() != age[i][j].ToString())
                        {
                            Console.WriteLine("Selected age value does not match with entered age value");
                            return false;
                        }
                    }

                    //for (int x = 3 - children[i] - 1; x >= 0; x--)
                    //{

                    //}

                    //for (int y = 2; y < 3 - children[i]; y--)
                    //{
                    //    if (DdlAge[y].Enabled)
                    //    {
                    //        return false;
                    //    }
                    //}
                    //var totalDisabled = 0;
                    //var lastDisabled = 2;
                    //while (totalDisabled < 3 - children[i])
                    //{
                    //    if (DdlAge[lastDisabled].Enabled)
                    //    {
                    //        Console.WriteLine($"Number {lastDisabled} Age of Children drop down list is not disabled.");
                    //        return false;
                    //    }
                    //    totalDisabled++;
                    //    lastDisabled--;
                    //}
                    int lastDisabled = 2 + count;
                    for (int totalDisabled = 0; totalDisabled < 3 - children[i]; totalDisabled++)
                    {
                        if (DdlAge[lastDisabled].Enabled)
                        {
                            Console.WriteLine($"Number {lastDisabled} Age of Children drop down list is not disabled.");
                            return false;
                        }
                        lastDisabled--;
                    }

                    int firstEnabled = 0 + count;
                    for (int totalEnabled = 0; totalEnabled < children[i]; totalEnabled++)
                    {
                        if (!DdlAge[firstEnabled].Enabled)
                        {
                            Console.WriteLine($"Number {firstEnabled} Age of Children drop down list is not enabled.");
                            return false;
                        }
                        firstEnabled++;
                    }
                    count += 3;
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

        public bool GetPostSelectedOccupants()
        {
            int totalAdults = 0;
            int totalChildren = 0;
            try
            {
                foreach (var item in adults)
                {
                    totalAdults += item;
                }

                foreach (var item in children)
                {
                    totalChildren += item;
                }

                string inputValue = $"{room} Rooms, {totalAdults + totalChildren} Guests";

                return TfSelectedOccupant.Text == inputValue ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public void Search(int time)
        {
            int tick = 0;
            try
            {
                BtnFind.Click();
                do
                {
                    Thread.Sleep(1000);
                    if (!driver.FindElement(By.CssSelector(".fullLoaderSpinner")).Displayed)
                    {
                        return;
                    }
                    tick++;
                } while (tick < time);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public void DragAndDrop()
        {
            Actions actions = new Actions(driver);
            actions.DragAndDrop(DragBox, DropBox).Perform();

        }
    }
}
