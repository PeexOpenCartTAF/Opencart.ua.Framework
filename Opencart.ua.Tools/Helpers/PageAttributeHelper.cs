using Opencart.ua.Tools.LogsHelpers;
using System.Configuration;

namespace Opencart.ua.Tools.Helpers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UrlAttribute : Attribute
    {
        public string Url { get; }

        public UrlAttribute(string url)
        {
            Url = url;
        }
    }

    public static class PageAttributeHelper
    {
        public static string GetPageUrl<T>()
        {
            OpenCartSeriLog.Info("Getiing URL from page attribute");
            var urlAttribute = (UrlAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(UrlAttribute));
            return ConfigurationManager.AppSettings["BaseUrl"] + urlAttribute?.Url;
        }
    }
}

