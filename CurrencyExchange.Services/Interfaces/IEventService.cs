using CurrencyExchange.Models;
using System.Threading.Tasks;

namespace CurrencyExchange.Services.Interfaces
{
    public interface IEventService
    {
        Task CreateEvent(EventType eventType, string eventDescription);
    }
}
