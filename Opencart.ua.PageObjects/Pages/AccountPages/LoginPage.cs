using Opencart.ua.PageObjects.Components;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.Helpers;
using Opencart.ua.Tools.LogsHelpers;
using OpenQA.Selenium;

namespace Opencart.ua.PageObjects.Pages.AccountPages
{
    [Url("/index.php?route=account/login")]
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
        public IWebElement LoginBtn => FindElement(By.CssSelector("input[type='submit']"));
        public IWebElement LoginErrorAlert => FindElement(By.CssSelector("div.alert.alert-danger"));
        public string LoginErrorAlertMessage => LoginErrorAlert.Text;

        public bool IsUserLoggedIn() => header.GetBreadcrumb().Equals("Обліковий запис");

        public void EnterCredsAndLogin(string email, string password)
        {
            OpenCartSeriLog.Info("Logging in...");
            EmailInputField.Clear();
            EmailInputField.SendKeys(email);
            PasswordInputField.Clear();
            PasswordInputField.SendKeys(password);
            LoginBtn.Click();
        }

        public void Logout()
        {
            try
            {
                if (IsUserLoggedIn())
                {
                    OpenCartSeriLog.Info("Logging out...");
                    rightMenu.ClickOnMenuItem(MenuItems.Logout);
                }
            }
            catch (Exception ex)
            {
                OpenCartSeriLog.Error($"Error during logout: {ex.Message}");
            }
        }
    }
}
