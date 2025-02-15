using Opencart.ua.PageObjects.Pages;
using Opencart.ua.Tools.Driver;
using OpenQA.Selenium;

namespace Opencart.ua.PageObjects.Components
{
    public enum MenuItems
    {
        Login,
        Register,
        ForgottenPassword,
        MyAccount,
        EditAccount,
        Password,
        AddressBook,
        WishList,
        OrderHistory,
        Downloads,
        Subscriptions,
        RewardPoints,
        Returns,
        Transactions,
        Newsletter,
        Logout
    }

    public interface IRightMenuComponent
    {
        IWebElement GetMenuItem(MenuItems menuItem);
        IList<IWebElement> GetAllMenuItems();
        void ClickOnMenuItem(MenuItems menuItem);
    }
    public class RightMenuComponent: BasePage, IRightMenuComponent
    {

        public RightMenuComponent(IWebDriverWrapper driver) : base(driver) { }

        public IWebElement GetMenuItem(MenuItems menuItem)
        {
            return FindElement(By.XPath($"//a[contains(@href, 'account/{menuItem.ToString().ToLower()}')]"));
        }

        public void ClickOnMenuItem(MenuItems menuItem)
        {
            GetMenuItem(menuItem).Click();
        }

        public IList<IWebElement> GetAllMenuItems()
        {
            return FindElements(By.XPath("//a[contains(@href, 'account/')]"));
        }
    }

}
