using CurrencyExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Services.Interfaces
{
    public interface INBPClient
    {
        Task<decimal> GetExchangeRate(string isoCode);
        Task<List<Currency>> GetAllCurrencies();
        Task<bool> CheckIfCurrencyExists(string isoCode);
        Task<List<CurrencyRate>> GetAllCurrenciesRate();
    }
}
