using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using LastMinuteTravel.Util;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace LastMinuteTravel.Settings
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    //[TestFixture(typeof(EdgeDriver))]

    class TestSetup<T> : ReportSetUp<T> where T : IWebDriver
    {
        private ExtentReports extReports;
        private ExtentHtmlReporter extReporter;
        protected ExtentTest extTest;

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            extReports = Reports;
            extReporter = Reporter;
            extReports.AttachReporter(extReporter);
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            extReports.Flush();
        }

        [SetUp]
        protected void SetUp()
        {
            string className = TestContext.CurrentContext.Test.ClassName.Split('.')[2].Split('`')[0];
            //string className = TestContext.CurrentContext.Test.ClassName;
            string testName = $"{TestContext.CurrentContext.Test.Name} ({typeof(T).ToString().Split('.')[2]})";
            //string testName = TestContext.CurrentContext.Test.Name;
            extTest = extReports.CreateTest(testName).AssignCategory(className);

            GetBrowserDriver();
            UtilFunctions<T>.GoToURL(ConfigurationManager.AppSettings["url"]);
        }

        [TearDown]
        protected void TearDown()
        {
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            string stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ?
                "" : TestContext.CurrentContext.Result.StackTrace;

            switch (status)
            {
                case TestStatus.Inconclusive:
                    break;
                case TestStatus.Skipped:
                    break;
                case TestStatus.Passed:
                    break;
                case TestStatus.Warning:
                    break;
                case TestStatus.Failed:
                    extTest.AddScreenCaptureFromPath(UtilFunctions<T>.TakeFailScreenshot());
                    break;
            }

            //extTest.Log(logStatus, $"{logStatus} {stackTrace}");

            if (Driver != null)
            {
                Driver.Quit();
            }
        }
    }
}
