using CurrencyExchange.Models;
using CurrencyExchange.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CurrencyExchangeContext _context;
        public Repository(CurrencyExchangeContext context)
        {
            _context = context;
        }
        public async Task Add(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(List<T> models)
        {
            await _context.Set<T>().AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
