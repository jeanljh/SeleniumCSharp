using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using LastMinuteTravel.Util;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Reflection;

namespace LastMinuteTravel.Settings
{
    class ReportSetUp<T> : DriverSetUp<T> where T : IWebDriver
    {
        protected static ExtentReports Reports { get; set; } = new ExtentReports();

        protected static ExtentHtmlReporter Reporter { get; set; } = GetReportName();


        //public static ExtentHtmlReporter Reporter { get; set; } = new ExtentHtmlReporter(@"C:\Users\Jean\Documents\LastMinuteTravel\LastMinuteTravel\Reports\TestResult.html");

        //public static ExtentHtmlReporter GetReportName()
        //{
        //    var rootDirectory = TestContext.CurrentContext.TestDirectory;
        //    var directory = rootDirectory.Substring(0, rootDirectory.IndexOf("bin")) + @"Reports\";
        //    var defaultFileName = TestContext.CurrentContext.Test.Name;
        //    var fileName = defaultFileName.Replace('<', '_').Remove(defaultFileName.LastIndexOf('>')) + ".html";
        //    return new ExtentHtmlReporter(directory + fileName);
        //}

        //public static ExtentHtmlReporter GetReportName()
        //{
        //    var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    var directory = Path.Combine(rootDirectory.Substring(0, rootDirectory.IndexOf("bin") - 1), "Reports", DateTime.Today.ToString("yyyy-MM-dd"));
        //    var fileName = $"Report_{DateTime.Now.ToString("HH.mm.ss")}.html";
        //    var filePath = Path.Combine(directory, fileName);

        //    try
        //    {
        //        Directory.CreateDirectory(directory);
        //        return new ExtentHtmlReporter(filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"GetReportName Error:\n{ex.Message}");
        //        throw;
        //    }
        //}

        public static ExtentHtmlReporter GetReportName()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string mainDir = Path.Combine(baseDir.Remove(baseDir.IndexOf("bin")), "Reports", DateTime.Now.ToString("yyyy-MM-dd"), typeof(T).ToString().Split('.')[2]);
            //string fileName = $"Report_{DateTime.Now.ToString("HH.mm.ss")}_{typeof(T).ToString().Split('.')[2]}";
            string fileName = $"Report_{DateTime.Now.ToString("HH.mm.ss")}";
            string fileDir = Path.Combine(mainDir, fileName);
            //Directory.CreateDirectory(fileDir).Attributes = FileAttributes.Hidden;
            try
            {
                Directory.CreateDirectory(fileDir);
                return new ExtentHtmlReporter(fileDir + @"\temp");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} error: {ex.Message}");
                throw;
            }

        }
    }
}
