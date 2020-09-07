using CurrencyExchange.Models;
using CurrencyExchange.Services.Interfaces;
using CurrencyExchange.Services.Repositories;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class CurrencyService : ICurrencyService
    {
        protected readonly INBPClient _nbpClient;
        protected readonly IEventService _eventService;
        protected readonly IValidator<Query> _queryValidator;
        public CurrencyService(INBPClient client,
            IEventService eventService,
            IValidator<Query> queryValidator)
        {
            _nbpClient = client;
            _eventService = eventService;
            _queryValidator = queryValidator;
        }

        public async Task<Result<decimal>> CalculateExchange(Query query)
        {
            decimal exhhangedAmount;
            decimal sourceCurrencyExchangeRate;
            decimal targetCurrencyExchangeRate;
            string eventDescription;
            var validationResult = await _queryValidator.ValidateAsync(query);
            if(validationResult.IsValid == false)
            {
                eventDescription = $"Calculate exchange called, validation failed with errors: {validationResult.Errors}";
                await _eventService.CreateEvent(EventType.ApiCall, eventDescription);
                return Result.Error<decimal>(validationResult.Errors);
            }
            eventDescription = $"Calculate exchange called, from {query.SourceCurrency} to {query.TargetCurrency}";

            if (query.SourceCurrency == "PLN")
            {
                targetCurrencyExchangeRate = await _nbpClient.GetExchangeRate(query.TargetCurrency);
                exhhangedAmount = query.Amount / targetCurrencyExchangeRate;
            }
            else if(query.TargetCurrency == "PLN")
            {
                sourceCurrencyExchangeRate = await _nbpClient.GetExchangeRate(query.SourceCurrency);
                exhhangedAmount = query.Amount * sourceCurrencyExchangeRate;
            }
            else 
            {
                sourceCurrencyExchangeRate = await _nbpClient.GetExchangeRate(query.SourceCurrency);
                targetCurrencyExchangeRate = await _nbpClient.GetExchangeRate(query.TargetCurrency);
                exhhangedAmount = query.Amount * sourceCurrencyExchangeRate / targetCurrencyExchangeRate;
            }
            await _eventService.CreateEvent(EventType.ApiCall, eventDescription);
            return Result.Ok(Math.Round(exhhangedAmount,2));
        }
    }
}
