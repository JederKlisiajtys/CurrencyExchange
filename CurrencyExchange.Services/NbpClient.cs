using CurrencyExchange.Models;
using CurrencyExchange.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class NbpClient : INBPClient
    {
        protected readonly IEventService _eventService;
        protected readonly IHttpClientFactory _clientFactory;
        public NbpClient(IEventService eventService,
            IHttpClientFactory clientFactory)
        {
            _eventService = eventService;
            _clientFactory = clientFactory;
        }
        public async Task<decimal> GetExchangeRate(string isoCode)
        {
            decimal exchangeRate;
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{isoCode}");
                
            string apiResponse = await response.Content.ReadAsStringAsync();
            var parsedObject = JObject.Parse(apiResponse);
            var popupJson = parsedObject["rates"][0]["mid"].ToString();
            exchangeRate = JsonConvert.DeserializeObject<decimal>(popupJson);

            var eventDescription = $"Get exchange rate for {isoCode}";
            await _eventService.CreateEvent(EventType.ExternalSerivceCall, eventDescription);
            return exchangeRate;
        }
        public async Task<List<Currency>> GetAllCurrencies()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://api.nbp.pl/api/exchangerates/tables/a");

            string apiResponse = await response.Content.ReadAsStringAsync();
            var parsedObject = JArray.Parse(apiResponse)[0].Value<JArray>("rates");
            var list = JsonConvert.DeserializeObject<List<Currency>>(parsedObject.ToString());
            list.Add(new Currency { Name = "polski złoty", ISOCode = "PLN" });
            var eventDescription = "Get all avaible currencies";
            await _eventService.CreateEvent(EventType.ExternalSerivceCall, eventDescription);
            return list;
        }

        public async Task<List<CurrencyRate>> GetAllCurrenciesRate()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://api.nbp.pl/api/exchangerates/tables/a");

            string apiResponse = await response.Content.ReadAsStringAsync();
            var parsedObject = JArray.Parse(apiResponse)[0].Value<JArray>("rates");
            var list = JsonConvert.DeserializeObject<List<CurrencyRate>>(parsedObject.ToString());
            var eventDescription = "Get all avaible currencies with rates";
            await _eventService.CreateEvent(EventType.ExternalSerivceCall, eventDescription);
            return list;
        }

        public async Task<bool> CheckIfCurrencyExists(string isoCode)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/a/{isoCode}");
            var eventDescription = response.StatusCode == System.Net.HttpStatusCode.OK ? $"Check if currency {isoCode} exists," +
                $" result:true" : $"Check if currency {isoCode} exists, result:false";

            await _eventService.CreateEvent(EventType.ExternalSerivceCall, eventDescription);

            return response.StatusCode == System.Net.HttpStatusCode.OK ? true : false;
        }
    }
}

