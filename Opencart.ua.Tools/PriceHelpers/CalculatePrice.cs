using System;

namespace Opencart.ua.Tools.PriceHelpers
{
    public static class CalculatePrice
    {
        private const double DOLLAR_VALUE = 1.00000000;
        private const double EURO_VALUE = 0.86552626;
        private const double POUND_VALUE = 0.74823518;

        public static double CalculatePriceValue(double defaultPriceValue, string currencyType)
        {
            return currencyType.ToUpper() switch
            {
                "USD" => defaultPriceValue * DOLLAR_VALUE,
                "EUR" => defaultPriceValue * EURO_VALUE,
                "GBP" => defaultPriceValue * POUND_VALUE,
                _ => throw new ArgumentException("Invalid currency"),
            };
        }

    }
}
