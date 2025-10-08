using Opencart.ua.Data;
using NUnit.Framework;
using Opencart.ua.PageObjects.Pages.AccountPages;
using Opencart.ua.Tools.Helpers;
using Opencart.ua.Tools.LogsHelpers;
using Opencart.ua.Tools.DBHelpers;

namespace Opencart.ua.Tests
{
    [TestFixture]
    //[Parallelizable(ParallelScope.All)]
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
            StartTimer();
            OpenCartSeriLog.Info("Opening Login page");
            loginPage.NavigateTo(loginPageUrl);
        }

        private static IEnumerable<object[]> LoginDataFromDb
        {
            get
            {
                var users = DataBaseHelper.GetAllUsers();
                foreach (var user in users)
                {
                    yield return new object[] 
                    {
                        user.Email,
                        user.Password 
                    };
                }
            }
        }

        [TestCaseSource(nameof(LoginDataFromDb)), Order(4)]
        public void CheckLoginDBData(string email, string password)
        {
            loginPage.EnterCredsAndLogin(email, password);

            Thread.Sleep(5000);
            switch (email)
            {
                case string wrong when wrong.Contains("wrong"):
                    Assert.That(!loginPage.IsUserLoggedIn());
                    break;
                case string success when success.Contains("ivanytsky"):
                    Assert.That(loginPage.IsUserLoggedIn());
                    break;
            }

        }

        private static IEnumerable<object[]> LoginData => new[]
        {
            new object[] 
            { 
                JsonData.GetJsonData().Users.SuccessUser.Email, 
                JsonData.GetJsonData().Users.SuccessUser.Password 
            },
            new object[] 
            { 
                JsonData.GetJsonData().Users.WrongUser.Email, 
                JsonData.GetJsonData().Users.WrongUser.Password 
            }
        };

        [TestCaseSource(nameof(LoginData)), Order(2)]
        public void CheckLogin(string email, string password)
        {
            loginPage.EnterCredsAndLogin(email, password);

            Thread.Sleep(5000);
            switch (email)
            {
                case string wrong when wrong.Contains("wrong"):
                    Assert.That(!loginPage.IsUserLoggedIn());
                    break;
                case string success when success.Contains("ivanytsky"):
                    Assert.That(loginPage.IsUserLoggedIn());
                    break;
            }

        }

        private static IEnumerable<object[]> WrongUserDataFromDb
        {
            get
            {
                var wrongUser = DataBaseHelper.GetUserByEmail("wrong@gmail.com");
                yield return new object[] 
                { 
                    wrongUser.Email,
                    wrongUser.Password,
                    "Warning: No match for E-Mail Address and/or Password." 
                };
            }
        }

        [TestCaseSource(nameof(WrongUserDataFromDb)), Order(3)]
        public void CheckAlertMessageDBData(string email, string password, string expectedMessage)
        {
            loginPage.EnterCredsAndLogin(email, password);
            Assert.That(loginPage.LoginErrorAlertMessage, Is.EqualTo(expectedMessage));
        }

        private static IEnumerable<object[]> WrongUserData => new[]
        {
            new object[]
            {
                JsonData.GetJsonData().Users.WrongUser.Email,
                JsonData.GetJsonData().Users.WrongUser.Password,
                "Warning: No match for E-Mail Address and/or Password."
            }
        };

        [TestCaseSource(nameof(WrongUserData)), Order(1)]
        public void CheckAlertMessage(string email, string password, string expectedMessage)
        {
            loginPage.EnterCredsAndLogin(email, password);
            Assert.That(loginPage.LoginErrorAlertMessage, Is.EqualTo(expectedMessage));
        }

        [TearDown]
        public void TearDown()
        {
            CollectResults();
        }

    }
}
