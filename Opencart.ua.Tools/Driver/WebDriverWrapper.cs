using Opencart.ua.Tools.LogsHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Opencart.ua.Tools.Driver
{
    public interface IWebDriverWrapper
    {
        void OpenUrl(string url);
        string GetUrl();
        IWebElement FindWebElement(By by);
        IEnumerable<IWebElement> FindWebElements(By by);
        void MoveToElement(IWebElement element);
        void QuitWebDriver();
    }

    public class WebDriverWrapper : IWebDriverWrapper
    {
        private readonly IWebDriver driver;
        public WebDriverWrapper()
        {
            OpenCartSeriLog.Debug("Preparing Chrome driver");
            try
            {
                driver = WebDriverInstance.GetDriver();
                OpenCartSeriLog.Debug("Chrome instance is created");
            }
            catch (DriveNotFoundException exception) 
            {
                OpenCartSeriLog.Error($"Driver not found: {exception.Message}");
            }
            catch (WebDriverException exception)
            {
                OpenCartSeriLog.Error($"Error during driver creation: {exception.Message}");
            }
            catch (Exception exception)
            {
                OpenCartSeriLog.Error($"Unexpected error: {exception.Message}");
            }
        }

        public void OpenUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public string GetUrl()
        {
            return driver.Url;
        }

        public IWebElement FindWebElement(By by)
        {
            IWebElement element = null;
            try
            {
                element = driver.FindElement(by);
            }
            catch (NoSuchElementException exception)
            {
                OpenCartSeriLog.Error($"Such element doesn`t exists: {exception.Message}");
            }
            return element;
        }

        public IEnumerable<IWebElement> FindWebElements(By by)
        {
            IEnumerable<IWebElement> elements = null;
            try
            {
                elements = driver.FindElements(by);
            }
            catch (NoSuchElementException exception)
            {
                OpenCartSeriLog.Error($"Such element doesn`t exists: {exception.Message}");
            }
            return elements;
        }

        public void MoveToElement(IWebElement element)
        {
            Actions actions = new(driver);
            try
            {
                actions.MoveToElement(element).Perform();
            }
            catch (NoSuchElementException exception)
            {
                OpenCartSeriLog.Error($"Such element doesn`t exists: {exception.Message}");
            }
        }

        public void QuitWebDriver()
        {
            OpenCartSeriLog.Debug("Closing driver");
            try
            {
                WebDriverInstance.QuitDriver();
                OpenCartSeriLog.Debug("WebDriver has been closed");
            }
            catch (WebDriverException exception)
            {
                OpenCartSeriLog.Error($"Error while closing the driver: {exception.Message}");
            }
        }
    }
}
