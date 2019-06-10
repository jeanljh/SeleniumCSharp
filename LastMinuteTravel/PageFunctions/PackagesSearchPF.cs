using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LastMinuteTravel.PageFunctions
{
    class PackagesSearchPF : MainSearchPO
    {
        public PackagesSearchPF(IWebDriver driver) : base(driver) { }

        public bool ValPreSelectedDropDownListOption()
        {
            try
            {
                TfSelectedOccupant.Click();

                if (new SelectElement(DdlOption[0]).SelectedOption.Text != "2 adults")
                {
                    Console.WriteLine("Expected = 2 adults" +
                        $"\nActual = {new SelectElement(DdlOption[0]).SelectedOption.Text}");
                    return false;
                }

                if (new SelectElement(DdlOption[1]).SelectedOption.Text != "0 children")
                {
                    Console.WriteLine("Expected = 0 children" +
                        $"\nActual = {new SelectElement(DdlOption[1]).SelectedOption.Text}");
                    return false;
                }

                if (new SelectElement(DdlOption[2]).SelectedOption.Text != "0 seniors")
                {
                    Console.WriteLine("Expected = 0 seniors" +
                        $"\nActual = {new SelectElement(DdlOption[2]).SelectedOption.Text}");
                    return false;
                }

                if (new SelectElement(DdlOption[3]).SelectedOption.Text != "0 infants")
                {
                    Console.WriteLine("Expected = 0 infants" +
                        $"\nActual = {new SelectElement(DdlOption[3]).SelectedOption.Text}");
                    return false;
                }

                for (int i = 4; i < DdlOption.Count; i++)
                {
                    if (new SelectElement(DdlOption[i]).SelectedOption.Text != "12")
                    {
                        Console.WriteLine($"Children age drop down list number {i - 3}:" +
                            $"\nExpected = 12" +
                            $"\nActual = {new SelectElement(DdlOption[i]).SelectedOption.Text}");
                        return false;
                    }
                    if (DdlOption[i].Enabled)
                    {
                        Console.WriteLine($"Children age drop down list number {i - 3} is enabled");
                        return false;
                    }
                }
                BtnCancel.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValDropDownListOptionCount()
        {
            try
            {
                TfSelectedOccupant.Click();
                new SelectElement(DdlOption[1]).SelectByIndex(3);

                int totalOptions = new SelectElement(DdlOption[0]).Options.Count;
                if (totalOptions != 4)
                {
                    Console.WriteLine("Adults drop down list:" +
                        "\nExpected = 4" +
                        $"\nActual = {totalOptions}");
                    return false;
                }

                totalOptions = new SelectElement(DdlOption[1]).Options.Count;
                if (totalOptions != 4)
                {
                    Console.WriteLine("Children drop down list:" +
                        "\nExpected = 4" +
                        $"\nActual = {totalOptions}");
                    return false;
                }

                totalOptions = new SelectElement(DdlOption[2]).Options.Count;
                if (totalOptions != 5)
                {
                    Console.WriteLine("Seniors drop down list:" +
                        "\nExpected = 5" +
                        $"\nActual = {totalOptions}");
                    return false;
                }

                totalOptions = new SelectElement(DdlOption[3]).Options.Count;
                if (totalOptions != 5)
                {
                    Console.WriteLine("Infants drop down list:" +
                        "\nExpected = 5" +
                        $"\nActual = {totalOptions}");
                    return false;
                }

                for (int i = 4; i < DdlOption.Count; i++)
                {
                    totalOptions = new SelectElement(DdlOption[i]).Options.Count;
                    if (totalOptions != 16)
                    {
                        Console.WriteLine($"Age of children drop down list {i - 3}:" +
                            $"\nExpected = 16" +
                            $"\nActual = {totalOptions}");
                        return false;
                    }
                }
                BtnCancel.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValDropDownListOptionValue()
        {
            int[] startIndex = { 1, 0, 0, 0, 2, 2, 2 };
            string[] word = { "adults", "children", "seniors", "infants", null, null, null };
            List<string> expectedOptions = new List<string>();

            try
            {
                TfSelectedOccupant.Click();
                new SelectElement(DdlOption[1]).SelectByIndex(3);
                for (int i = 0; i < 7; i++)
                {
                    SelectElement comboBox = new SelectElement(DdlOption[i]);
                    for (int j = startIndex[i]; j < comboBox.Options.Count + startIndex[i]; j++)
                    {
                        expectedOptions.Add($"{j} {FilterWord(j, word[i])}".Trim());
                    }
                    if (!expectedOptions.SequenceEqual(comboBox.Options.Select(x => x.Text)))
                    {
                        Console.WriteLine("Expected option list does not match with actual option list");
                        return false;
                    }
                    expectedOptions.Clear();
                }
                BtnCancel.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValCancelRoomAddition()
        {
            int count = 0;
            try
            {
                TfSelectedOccupant.Click();
                do
                {
                    try
                    {
                        if (BtnDeleteRoom[0].Displayed)
                        {
                            Console.WriteLine("Delete room button exists in default state.");
                            return false;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        BtnAddRoom.Click();
                        if (BtnDeleteRoom.Count != 2)
                        {
                            Console.WriteLine("Delete room button count is not 2");
                            return false;
                        }
                    }
                    BtnDeleteRoom[count].Click();
                    count++;
                } while (count < 2);
                BtnCancel.Click();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool SelectRoomAndGuest(int room, int[] adults, int[] children, int[] seniors, int[] infants, List<int[]> age)
        {
            int count = 0;
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", TfSelectedOccupant);

                for (int i = 0; i < room; i++)
                {
                    if (i > 0) BtnAddRoom.Click();

                    string chosenValue = $"{adults[i]} {FilterWord(adults[i], "adults")}";
                    SelectElement comboBox = new SelectElement(DdlOption[0 + count]);
                    comboBox.SelectByValue(adults[i].ToString());

                    if (comboBox.SelectedOption.Text != chosenValue)
                    {
                        Console.WriteLine($"Chosen total adults = {chosenValue}" +
                            $"\nSelected total adults = {comboBox.SelectedOption.Text}");
                        return false;
                    }

                    chosenValue = $"{children[i]} {FilterWord(children[i], "children")}";
                    comboBox = new SelectElement(DdlOption[1 + count]);
                    comboBox.SelectByValue(children[i].ToString());

                    if (comboBox.SelectedOption.Text != chosenValue)
                    {
                        Console.WriteLine($"Chosen total children = {chosenValue}" +
                            $"\nSelected total children = {comboBox.SelectedOption.Text}");
                        return false;
                    }

                    chosenValue = $"{seniors[i]} {FilterWord(seniors[i], "seniors")}";
                    comboBox = new SelectElement(DdlOption[2 + count]);
                    comboBox.SelectByValue(seniors[i].ToString());

                    if (comboBox.SelectedOption.Text != chosenValue)
                    {
                        Console.WriteLine($"Chosen total seniors = {chosenValue}" +
                            $"\nSelected total seniors = {comboBox.SelectedOption.Text}");
                        return false;
                    }

                    chosenValue = $"{infants[i]} {FilterWord(infants[i], "infants")}";
                    comboBox = new SelectElement(DdlOption[3 + count]);
                    comboBox.SelectByValue(infants[i].ToString());

                    if (comboBox.SelectedOption.Text != chosenValue)
                    {
                        Console.WriteLine($"Chosen total infants = {chosenValue}" +
                            $"\nSelected total infants = {comboBox.SelectedOption.Text}");
                        return false;
                    }

                    for (int j = 0; j < children[i]; j++)
                    {
                        comboBox = new SelectElement(DdlOption[j + 4 + count]);
                        comboBox.SelectByValue(age[i][j].ToString());
                        if (comboBox.SelectedOption.Text != age[i][j].ToString())
                        {
                            Console.WriteLine($"Chosen age for Age drop down list {j + 1} = {age[i][j].ToString()}" +
                                $"\nSelected age = {comboBox.SelectedOption.Text}");
                            return false;
                        }
                    }
                    if (!ValChildrenAgeEnabledState(children[i], 4 + count, DdlOption.Count - 1))
                    {
                        Console.WriteLine("Age of Children drop down list is not in correct enabled / disabled state");
                        return false;
                    }
                    count += 7;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public string FilterWord(int total, string word)
        {
            try
            {
                return total == 1 ?
                    word.Last() == 's' ? word.TrimEnd('s') : word.Remove(word.IndexOf('r'))
                    : word;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public bool ValChildrenAgeEnabledState(int totalChildren, int ddlStartIndex, int ddlEndIndex)
        {
            try
            {
                if (totalChildren == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (DdlOption[ddlStartIndex + i].Enabled)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    int totalDisabled = 3 - totalChildren;
                    for (int i = 0; i < totalChildren; i++)
                    {
                        if (!DdlOption[ddlStartIndex + i].Enabled)
                        {
                            return false;
                        }
                    }
                    for (int i = totalDisabled - 1; i >= 0; i--)
                    {
                        if (DdlOption[ddlEndIndex - i].Enabled)
                        {
                            return false;
                        }
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
    }
}
