using Newtonsoft.Json;

namespace LogicPower.ua.Data
{
    public class User
    {
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }
        [JsonProperty("last_name")]
        public string? LastName { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("password")]
        public string? Password { get; set; }
    }

    public class Users
    {
        [JsonProperty("success_user")]
        public User? SuccessUser { get; set; }
        [JsonProperty("wrong_user")]
        public User? WrongUser { get; set; }
    }

    public class JsonData
    {
        public Users? Users { get; set; }

        public static JsonData GetJsonData()
        {
            string filePath = $"{Directory.GetCurrentDirectory()}\\data.json";

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                var jsonObject = JsonConvert.DeserializeObject<JsonData>(jsonData);
                return jsonObject ?? throw new InvalidOperationException("Failed to deserialize JSON data.");
            }
            throw new FileNotFoundException($"JSON file not found at {filePath}");
        }

    }
}