using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Configuration;
using OpenQA.Selenium.Firefox;

namespace Opencart.ua.Tools.Driver
{
    public class WebDriverInstance
    {
        private static IWebDriver driver;
        private static readonly string browser = ConfigurationManager.AppSettings["Browser"];
        private static readonly int implicitWait = int.Parse(ConfigurationManager.AppSettings["ImplicitWait"]);
        private static readonly int pageLoadTimeout = int.Parse(ConfigurationManager.AppSettings["PageLoadTimeout"]);

        private WebDriverInstance() { }

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                switch (browser)
                {
                    case "Chrome":
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--no-sandbox");
                        chromeOptions.AddArgument("--disable-dev-shm-usage");
                        chromeOptions.AddArgument("--disable-gpu");
                        driver = new ChromeDriver(chromeOptions);
                        break;

                    case "Firefox":
                        var firefoxOptions = new FirefoxOptions();
                        firefoxOptions.AddArgument("--no-sandbox");
                        firefoxOptions.AddArgument("--disable-dev-shm-usage");
                        firefoxOptions.AddArgument("--disable-gpu");
                        driver = new FirefoxDriver(firefoxOptions);
                        break;

                    default:
                        throw new NotSupportedException($"Browser '{browser}' is not supported.");
                }

                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
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
