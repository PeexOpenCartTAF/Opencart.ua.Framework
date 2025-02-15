using Opencart.ua.PageObjects.Pages;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.LogsHelpers;
using OpenQA.Selenium;

namespace Opencart.ua.PageObjects.Components
{
    internal class TopMenuComponent : BasePage
    {
        public TopMenuComponent(IWebDriverWrapper driver) : base(driver){}

        private readonly Dictionary<string, string> CurrencyOptions = new()
        {
            {"EUR", "//a[@href = 'EUR']"},
            {"GBP", "//a[@href = 'GBP']"},
            {"USD", "//a[@href = 'USD']"}
        };

        private readonly Dictionary<string, string> MyAccountOptions = new()
        {
            {"Register", "ul.dropdown-menu > li > a[href *= 'account/register']"},
            {"Login", "ul.dropdown-menu > li > a[href *= 'account/login']"},
            {"MyAccount", "ul.dropdown-menu > li > a[href *= 'account/account']"},
            {"OrderHistory", "ul.dropdown-menu > li > a[href *= 'account/order']"},
            {"Transactions", "ul.dropdown-menu > li > a[href *= 'account/transaction']"},
            {"Downloads", "ul.dropdown-menu > li > a[href *= 'account/download']"},
            {"Logout", "ul.dropdown-menu > li > a[href *= 'account/logout']"}
        };

        private IWebElement CurrencyDropdown => FindElement(By.CssSelector("#form-currency > div > a > i"));
        private IWebElement ContactInfoButton => FindElement(By.XPath("//ul[@class = 'list-inline']/li/a[contains(@href, 'information/contact')]"));
        private IWebElement ContactInfoPhoneNumber => FindElement(By.CssSelector("ul.list-inline > li > span"));
        private IWebElement MyAccountDropdown => FindElement(By.CssSelector(".list-inline-item > div > a > i.fa-caret-down"));
        private IWebElement WishListButton => FindElement(By.Id("wishlist-total"));
        private IWebElement ShoppingCartButton => FindElement(By.XPath("//a[@title = 'Shopping Cart']"));
        private IWebElement CheckoutButton => FindElement(By.XPath("//a[@title = 'Checkout']"));

        private IWebElement GetCurrencyDropdownOption(string currency)
        {
            return FindElement(By.XPath(CurrencyOptions[currency]));
        }

        private IWebElement GetMyAccountDropdownOption(string accountOption)
        {
            return FindElement(By.CssSelector(MyAccountOptions[accountOption]));
        }

        public void SelectCurrency(string currency)
        {
            CurrencyDropdown.Click();
            GetCurrencyDropdownOption(currency).Click();
            Console.WriteLine($"Selected {currency} from Top Menu");
        }

        public void SelectMyAccountOption(string accountOption)
        {
            MyAccountDropdown.Click();
            GetMyAccountDropdownOption(accountOption).Click();
        }

        public void OpenContactInfo()
        {
            ContactInfoButton.Click();
        }

        public string GetContactPhoneNumber()
        { 
            return ContactInfoPhoneNumber.Text;
        }

        public void OpenWishList()
        {
            WishListButton.Click();
        }

        public void OpenShoppingCart() 
        {
            ShoppingCartButton.Click();
        }

        public void OpenCheckout()
        {
            CheckoutButton.Click();
        }

    }

    internal class SearchComponent : BasePage
    {
        public SearchComponent(IWebDriverWrapper driver) : base(driver) { }

        private IWebElement SearchField => FindElement(By.Name("search"));
        private IWebElement SearchBtn => FindElement(By.CssSelector(".btn.btn-light.btn-lg"));
        private IWebElement Logo => FindElement(By.CssSelector("#logo > a"));
        private IWebElement HeaderCart => FindElement(By.CssSelector("#header-cart > div > button"));

        public void ClickOnLogo()
        { 
            Logo.Click();
        }

        public void PerformSearch(string query)
        {
            SearchField.Clear();
            SearchField.SendKeys(query);
            SearchBtn.Click();
        }

        public void OpenHeaderCartDialog()
        {
            HeaderCart.Click();
        }
    }

    internal class MegaMenuComponent : BasePage
    {
        public MegaMenuComponent(IWebDriverWrapper driver) : base(driver) { }

        private IWebElement GetMenuOptionElement(string optionName)
        {
            return FindElement(By.LinkText(optionName));
        }

        private void HoverOverMenuItem(string menuOptionName)
        {
            MoveCursorToElement(GetMenuOptionElement(menuOptionName));
        }

        public void SelectMenuOption(string menuOptionName, string subMenuOptionName)
        {
            HoverOverMenuItem(menuOptionName);
            GetMenuOptionElement(subMenuOptionName).Click();
        }
    }

    internal class BreadcrumbsComponent : BasePage
    {
        public BreadcrumbsComponent(IWebDriverWrapper driver) : base(driver) { }

        public void SelectBreadcrumbByName(string breadcrumbName)
        { 
            FindElement(By.XPath($"//ul[@class='breadcrumb']/li/a[contains(text(), '{breadcrumbName}')]")).Click();
        }

        public string GetLastBreadcrumbName()
        {
            var breadcrumbs = FindElements(By.CssSelector("ul.breadcrumb > li > a"));
            return breadcrumbs.LastOrDefault().Text;
        }

    }

    public interface IHeaderComponents
    {
        void SearchForItem(string query);
        void SelectCurrencyFromTopMenu(string currency);
        void SelectMyAccountOptionFromTopMenu(string accountOption);
        void SelectMegaMenuOption(string menuOptionName, string subMenuOptionName);
        void NavigateToHomePageByLogo();
        void SelectBreadcrumb(string breadcrumb);
        string GetBreadcrumb();
    }

    // Facade class which combine all Header components
    public class HeaderComponents : IHeaderComponents
    {
        private readonly TopMenuComponent _topMenu;
        private readonly SearchComponent _search;
        private readonly MegaMenuComponent _megaMenu;
        private readonly BreadcrumbsComponent _breadcrumbs;

        public HeaderComponents(IWebDriverWrapper driver)
        {
            _topMenu = new TopMenuComponent(driver);
            _search = new SearchComponent(driver);
            _megaMenu = new MegaMenuComponent(driver);
            _breadcrumbs = new BreadcrumbsComponent(driver);
        }

        public void SearchForItem(string query)
        {
            _search.PerformSearch(query);
        }

        public void SelectCurrencyFromTopMenu(string currency)
        {
            _topMenu.SelectCurrency(currency);
        }

        public void SelectMyAccountOptionFromTopMenu(string accountOption)
        {
            _topMenu.SelectMyAccountOption(accountOption);
        }

        public void SelectMegaMenuOption(string menuOptionName, string subMenuOptionName)
        {
            _megaMenu.SelectMenuOption(menuOptionName, subMenuOptionName);
        }

        public void NavigateToHomePageByLogo() 
        {
            _search.ClickOnLogo();
        }

        public void SelectBreadcrumb(string breadcrumb)
        {
            _breadcrumbs.SelectBreadcrumbByName(breadcrumb);
        }

        public string GetBreadcrumb()
        {
            return _breadcrumbs.GetLastBreadcrumbName();
        }
    }

}
