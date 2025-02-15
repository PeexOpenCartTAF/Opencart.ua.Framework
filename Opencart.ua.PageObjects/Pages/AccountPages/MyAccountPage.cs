using Opencart.ua.PageObjects.Components;
using Opencart.ua.Tools.Driver;

namespace Opencart.ua.PageObjects.Pages.AccountPages
{

    public class MyAccountPage : BasePage
    {
        private readonly IRightMenuComponent rightMenu;

        public MyAccountPage(IWebDriverWrapper driver) : base(driver)
        {
            rightMenu = new RightMenuComponent(driver);
        }

    }
}
