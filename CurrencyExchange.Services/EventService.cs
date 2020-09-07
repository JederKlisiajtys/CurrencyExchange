using CurrencyExchange.Models;
using CurrencyExchange.Services.Interfaces;
using CurrencyExchange.Services.Repositories;
using System;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class EventService : IEventService
    {
        protected readonly IEventRepository _repository;
        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateEvent(EventType eventType, string eventDescription)
        {
            var systemEvent = new Event
            {
                DateCreated = DateTime.Now,
                EventType = eventType,
                EventDescription = eventDescription
            };
            await _repository.Add(systemEvent);
        }
    }
}
