using Opencart.ua.PageObjects.Components;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.Helpers;
using OpenQA.Selenium;

namespace Opencart.ua.PageObjects.Pages
{
    [Url("/en-gb?route=common/home")]
    public class HomePage : BasePage
    {
        private readonly IHeaderComponents header;

        public HomePage(IWebDriverWrapper driver) : base(driver)
        {
            header = new HeaderComponents(driver);
        }

        public IWebElement HdrTitle => FindElement(By.XPath("//div[@class='app_logo']"));

        public void SearchForProduct(string query)
        {
            header.SearchForItem(query);
        }

        public void SelectLoginTopMenuOption(string option)
        {
            header.SelectMyAccountOptionFromTopMenu(option);
        }

        public void SelectMenuOption(string menuName, string subMenuName)
        {
            header.SelectMegaMenuOption(menuName, subMenuName);
        }

        public void SelectBreadcrumb(string breadcrumb)
        { 
            header.SelectBreadcrumb(breadcrumb);
        }

        public string GetBreadcrumb()
        {
            return header.GetBreadcrumb();
        }

    }
}
