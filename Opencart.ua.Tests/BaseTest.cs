using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.Reporting;
using Opencart.ua.Tools.ScreenshotHelpers;
using System.Diagnostics;

namespace Opencart.ua.Tests
{
    [SetUpFixture]
    //[Parallelizable(ParallelScope.All)]
    public class BaseTest
    {
        
        protected IWebDriverWrapper driver;
        protected static List<TestResult> results = new();
        protected Stopwatch stopwatch;

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            driver = new WebDriverWrapper();
        }

        protected void StartTimer()
        {
            stopwatch = Stopwatch.StartNew();
        }

        protected void CollectResults()
        {
            string screenshotPath = TakeScreenshotHelper.TakeScreenshot();
            stopwatch.Stop();
            var context = TestContext.CurrentContext;
            results.Add(new TestResult
            {
                TestName = context.Test.Name,
                Status = context.Result.Outcome.Status.ToString(),
                Duration = stopwatch.Elapsed.TotalMilliseconds,
                ErrorMessage = context.Result.FailCount > 0 ? context.Result.Message : "",
                ScreenshotPath = screenshotPath
            });
            
        }

        protected void SaveReport()
        {
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Reporting\\custom-report.html");
            CustomHtmlReportGenerator.Generate(results, outputPath);
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            SaveReport();
            driver.QuitWebDriver();
        }
    }
}
