using Opencart.ua.PageObjects.Components;
using Opencart.ua.Tools.Driver;

namespace Opencart.ua.PageObjects.Pages.AccountPages
{
    public class RegisterPage : BasePage
    {
        private readonly IRightMenuComponent rightMenu;

        public RegisterPage(IWebDriverWrapper driver) : base(driver)
        {
            rightMenu = new RightMenuComponent(driver);
        }
    }
}
