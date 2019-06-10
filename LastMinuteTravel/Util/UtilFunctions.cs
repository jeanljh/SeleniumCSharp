using LastMinuteTravel.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace LastMinuteTravel.Util
{
    class UtilFunctions<TWebDriver> : DriverSetUp<TWebDriver> where TWebDriver : IWebDriver
    {
        public static void GoToURL(string url, int pageLoadTimeOut = 30, int implicitWaitTimeOut = 30)
        {
            try
            {
                Driver.Navigate().GoToUrl(url);
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeOut);
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTimeOut);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public static bool WaitUntilElementInvisible(int timeout, By by)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (ElementNotVisibleException ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public static void WaitUntilElementVisible(int timeout, By by)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }

        public static int GenerateRandonNumber(int count) => new Random().Next(count);

        public static void WaitForJQueryElementInvisible(string cssPath, int waitSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds));
            wait.Until(Driver =>
            {
                bool isAjaxFinished = (bool)((IJavaScriptExecutor)Driver)
                .ExecuteScript("return jQuery.active == 0");
                bool isLoaderHidden = (bool)((IJavaScriptExecutor)Driver)
                .ExecuteScript($"return $('{cssPath}').is(':visible') == false");
                return isAjaxFinished & isLoaderHidden;
            });
        }

        public static string TakeFailScreenshot()
        {
            string rootDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderDir = Path.Combine(rootDir.Remove(rootDir.IndexOf("bin")), "Screenshots", DateTime.Today.ToString("yyyy-MM-dd"));
            string fileName = $"Screenshot_{DateTime.Now.ToString("HH.mm.ss")}.jpeg";
            string filePath = Path.Combine(folderDir, fileName);

            try
            {
                Directory.CreateDirectory(folderDir);
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                ss.SaveAsFile(filePath, ScreenshotImageFormat.Jpeg);
                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }
        }
    }
}
