using NUnit.Framework;
using Opencart.ua.Tools.Driver;

namespace Opencart.ua.Tests
{
    [SetUpFixture]
    public class BaseTest
    {

        protected IWebDriverWrapper driver;

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            driver = new WebDriverWrapper();

        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            driver.QuitWebDriver();
        }
    }
}
