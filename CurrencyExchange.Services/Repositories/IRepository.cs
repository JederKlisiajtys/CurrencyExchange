using CurrencyExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Services.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T model);
        Task AddRange(List<T> models);
        Task<List<T>> GetAll();
    }
}
