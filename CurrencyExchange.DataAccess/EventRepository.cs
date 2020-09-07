using CurrencyExchange.Models;
using CurrencyExchange.Services.Repositories;

namespace CurrencyExchange.DataAccess
{
    public class EventRepository : Repository<Event>,
        IEventRepository
    {
        public EventRepository(CurrencyExchangeContext context)
            :base(context)
        {

        }
    }
}
