using System.Threading.Tasks;

namespace CurrencyExchange.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<decimal>> CalculateExchange(Query query);
    }
}
