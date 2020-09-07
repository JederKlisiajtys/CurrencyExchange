using Newtonsoft.Json;

namespace CurrencyExchange.Services
{
    public class Query
    {
        private string _sourceCurrency;
        public string SourceCurrency { get { return _sourceCurrency.ToUpper(); } set { _sourceCurrency = value; } }

        private string _targetCurrency;
        public string TargetCurrency { get { return _targetCurrency.ToUpper(); } set { _targetCurrency = value; } }
        public decimal Amount { get; set; }
    }
}
