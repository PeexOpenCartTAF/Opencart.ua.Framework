using Opencart.ua.PageObjects.Components;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.Helpers;
using OpenQA.Selenium;

namespace Opencart.ua.PageObjects.Pages.AccountPages
{
    [Url("/en-gb?route=account/login")]
    public class LoginPage : BasePage
    {
        private readonly IRightMenuComponent rightMenu;
        private readonly IHeaderComponents header;

        public LoginPage(IWebDriverWrapper driver) : base(driver)
        {
            header = new HeaderComponents(driver);
            rightMenu = new RightMenuComponent(driver);
        }

        public IWebElement EmailInputField => FindElement(By.Id("input-email"));
        public IWebElement PasswordInputField => FindElement(By.Id("input-password"));
        public IWebElement LoginBtn => FindElement(By.CssSelector("div.text-end > button.btn.btn-primary"));
        public IWebElement LoginErrorAlert => FindElement(By.CssSelector("#alert .alert-danger"));
        public string LoginErrorAlertMessage => LoginErrorAlert.Text;

        public bool IsUserLoggedIn() => header.GetBreadcrumb().Equals("Account");

        public void EnterCredsAndLogin(string email, string password)
        {
            EmailInputField.Clear();
            EmailInputField.SendKeys(email);
            PasswordInputField.Clear();
            PasswordInputField.SendKeys(password);
            LoginBtn.Click();
        }

    }
}
