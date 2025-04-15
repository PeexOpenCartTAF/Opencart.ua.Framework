using Opencart.ua.PageObjects.Components;
using Opencart.ua.Tools;
using Opencart.ua.Tools.Driver;
using Opencart.ua.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opencart.ua.PageObjects.Pages
{
    [Url("/en-gb?route=product/search")]
    public class SearchPage : BasePage
    {
        private readonly IHeaderComponents _header;
        public SearchPage(IWebDriverWrapper driver) : base(driver) 
        {
            _header = new HeaderComponents(driver);
        }
    }
}
