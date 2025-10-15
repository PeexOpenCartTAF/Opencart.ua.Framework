using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.LogsHelpers;
using OpenQA.Selenium;

namespace Opencart.ua.PageObjects.Pages
{
    public abstract class BasePage
    {
        private readonly IWebDriverWrapper _driver;

        public BasePage(IWebDriverWrapper driver)
        {
            _driver = driver;
        }

        public void NavigateTo(string Url)
        {
            OpenCartSeriLog.Debug($"Navigating to URL: {Url}");
            _driver.OpenUrl(Url);
        }

        public void WaitForPageToLoad()
        {
            OpenCartSeriLog.Debug("Waiting for page to load");
            _driver.WaitForPageToLoad();
        }

        public string GetCurrentUrl()
        {
            OpenCartSeriLog.Debug("Getting current URL");
            return _driver.GetUrl();
        }

        public IWebElement FindElement(By by)
        {
            OpenCartSeriLog.Debug($"Finding element by: {by}");
            return _driver.FindWebElement(by);
        }

        public IList<IWebElement> FindElements(By by) 
        {
            OpenCartSeriLog.Debug($"Finding elements by: {by}");
            var elements = _driver.FindWebElements(by);
            return elements.ToList();
        }

        public void MoveCursorToElement(IWebElement element)
        {
            OpenCartSeriLog.Debug($"Moving cursor to element: {element}");
            _driver.MoveToElement(element);
        }

        public string GetTextFromElement(IWebElement element)
        {
            OpenCartSeriLog.Debug($"Getting text from element: {element}");
            return element.Text;
        }

        public static bool IsElementPresent(IWebElement element)
        {
            OpenCartSeriLog.Debug($"Checking if element present: {element}");
            if (element == null)
            {
                return false;
            }
            return true;
        }

    }
}
