using System;

namespace CurrencyExchange.Models
{
    public class Event 
    {
        public Guid Id { get; set; }
        public string EventDescription { get; set; }
        public EventType EventType { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public enum EventType
    {
        ApiCall,
        ExternalSerivceCall
    }
}
