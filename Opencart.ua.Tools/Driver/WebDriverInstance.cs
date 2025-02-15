using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Opencart.ua.Tools.Driver
{
    public class WebDriverInstance
    {
        private static IWebDriver driver;

        private WebDriverInstance() { }

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-gpu");
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            return driver;
        }

        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

    }
}
