using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace LastMinuteTravel.Settings
{
    public class DriverSetUp<T> where T : IWebDriver
    {
        public static IWebDriver Driver { get; private set; }

        public static IWebDriver GetBrowserDriver()
        {
            if (typeof(T) == typeof(ChromeDriver))
            {
                ChromeOptions option = new ChromeOptions();
                option.AddArgument("--start-maximized");
                //option.AddArgument("--headless");
                //option.AddArgument("--window-size=1382,744");
                Driver = new ChromeDriver(option);
            }
            else if (typeof(T) == typeof(FirefoxDriver))
            {
                Driver = new FirefoxDriver();
            }
            else if (typeof(T) == typeof(EdgeDriver))
            {
                EdgeOptions options = new EdgeOptions
                {
                    UseInPrivateBrowsing = true
                };
                Driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(@"C:\Windows\SysWOW64\", "MicrosoftWebDriver.exe", 52296), options);
            }
            return Driver;
        }
    }
}
