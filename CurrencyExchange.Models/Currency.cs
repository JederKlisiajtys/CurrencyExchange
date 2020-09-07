using Newtonsoft.Json;

namespace CurrencyExchange.Models
{
    public class Currency 
    {
        [JsonProperty("code")]
        public string ISOCode { get; set; }
        [JsonProperty("currency")]
        public string Name { get; set; }
    }
    public class CurrencyRate : Currency
    {
        [JsonProperty("mid")]
        public decimal Rate { get; set; }
    }
}
