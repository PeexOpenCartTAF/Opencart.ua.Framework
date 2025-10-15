using Newtonsoft.Json;

namespace Opencart.ua.Tools.PriceHelpers
{
    public class CurrencyRateModel
    {
        [JsonProperty("cc")]
        public string CurrencyCode { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
    }

    public static class CalculateCurrencyRate
    {
        private const string NbuApiUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

        public static async Task<double> GetCurrencyRate(string currencyCode)
        {
            using var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(NbuApiUrl);
            var rates = JsonConvert.DeserializeObject<CurrencyRateModel[]>(json);
            var rate = rates.FirstOrDefault(r => r.CurrencyCode.Equals(currencyCode, StringComparison.OrdinalIgnoreCase));

            return rate == null ? throw new Exception($"Exchange rate for {currencyCode} was not found.") : rate.Rate;
        }
    }
}
