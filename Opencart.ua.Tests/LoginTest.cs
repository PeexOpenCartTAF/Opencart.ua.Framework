using LogicPower.ua.Data;
using NUnit.Framework;
using Opencart.ua.PageObjects.Pages.AccountPages;
using Opencart.ua.Tools.Helpers;
using Opencart.ua.Tools.LogsHelpers;

namespace Opencart.ua.Tests
{
    [TestFixture]
    public class LoginTest : BaseTest
    {
        private LoginPage loginPage;
        private string loginPageUrl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            OpenCartSeriLog.Info("Preparing proper pages");
            loginPage = new LoginPage(driver);
            loginPageUrl = PageAttributeHelper.GetPageUrl<LoginPage>();
        }

        [SetUp]
        public void Setup()
        {
            OpenCartSeriLog.Info("Opening Login page");
            loginPage.NavigateTo(loginPageUrl);
        }

        private static IEnumerable<object[]> LoginData => new[]{
            new object[] { JsonData.GetJsonData().Users.SuccessUser.Email, JsonData.GetJsonData().Users.SuccessUser.Password },
            new object[] { JsonData.GetJsonData().Users.WrongUser.Email, JsonData.GetJsonData().Users.WrongUser.Password }
        };

        [TestCaseSource(nameof(LoginData)), Order(1)]
        public void Login(string email, string password)
        {
            loginPage.EnterCredsAndLogin(email, password);

            OpenCartSeriLog.Info($"Input Data: user={email} pass={password}");
            Thread.Sleep(5000);
            switch (email)
            {
                case string wrong when wrong.Contains("wrong"):
                    Assert.That(email.Equals("wrong@gmail.com"));
                    Assert.That(password.Equals("123wrongAS$%"));
                    Assert.That(!loginPage.IsUserLoggedIn());
                    OpenCartSeriLog.Info("###### ALERT MESSAGE: " + loginPage.LoginErrorAlertMessage);
                    OpenCartSeriLog.Info("###### ALERT MESSAGE: TEST push to branch" + loginPage.LoginErrorAlertMessage);
                    break;
                case string success when success.Contains("ivanytsky"):
                    Assert.That(email.Equals("y.ivanytsky@gmail.com"));
                    Assert.That(password.Equals("123qweAS$%"));
                    Assert.That(loginPage.IsUserLoggedIn());
                    break;
            }
            
        }

    }
}
