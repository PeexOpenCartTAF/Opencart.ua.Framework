using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.LogsHelpers;
using OpenQA.Selenium;

namespace Opencart.ua.Tools.ScreenshotHelpers
{

    public static class TakeScreenshotHelper
    {
        public static string TakeScreenshot()
        {
            try
            {
                IWebDriver driver = WebDriverInstance.GetDriver();
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Reporting\\Screenshots");
                string testName = TestContext.CurrentContext.Test.MethodName;
                Directory.CreateDirectory(path);
                var filePath = Path.Combine(path, $"{testName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
                screenshot.SaveAsFile(filePath);
                OpenCartSeriLog.Debug($"Screenshot saved: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                OpenCartSeriLog.Error($"Failed to take screenshot: {ex.Message}");
                return null;
            }
        }
    }

}
