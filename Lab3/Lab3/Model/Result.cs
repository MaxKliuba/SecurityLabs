using System.Text.Json.Serialization;

namespace Lab3.Model
{
    class Result
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("account")]
        public Account Account { get; set; }

        [JsonPropertyName("realNumber")]
        public long RealNumber { get; set; }

        public override string ToString()
        {
            return $"[[message]: {Message}; [account]: {Account}; [realNumber]: {RealNumber}]";
        }
    }
}
