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
                string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Reporting", "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                string testName = TestContext.CurrentContext.Test.MethodName;
                string fileName = $"{testName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                string filePath = Path.Combine(screenshotsDir, fileName);

                screenshot.SaveAsFile(filePath);
                OpenCartSeriLog.Debug($"Screenshot saved: {filePath}");

                string relativePath = Path.Combine("Screenshots", fileName).Replace("\\", "/");
                return relativePath;
            }
            catch (Exception ex)
            {
                OpenCartSeriLog.Error($"Failed to take screenshot: {ex.Message}");
                return null;
            }
        }
    }

}
