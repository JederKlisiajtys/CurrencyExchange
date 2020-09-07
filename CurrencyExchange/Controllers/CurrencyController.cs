using CurrencyExchange.Models;
using CurrencyExchange.Services;
using CurrencyExchange.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        protected readonly ICurrencyService _currencyService;
        protected readonly INBPClient _nbpClient;
        public CurrencyController(ICurrencyService currencyService,
            INBPClient nbpClient)
        {
            _currencyService = currencyService;
            _nbpClient = nbpClient;
        }

        [ProducesResponseType(200, Type = typeof(List<Currency>))]
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await _nbpClient.GetAllCurrencies();
            return Ok(currencies);
        }

        [ProducesResponseType(200, Type = typeof(List<CurrencyRate>))]
        [HttpGet("rates")]
        public async Task<IActionResult> GetAllRates()
        {
            var currencies = await _nbpClient.GetAllCurrenciesRate();
            return Ok(currencies);
        }

        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        [HttpGet("exchange")]
        public async Task<IActionResult> ExchangeAmount([FromQuery] Query query)
        {
            var result = await _currencyService.CalculateExchange(query);
            if(result.Success == false)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
    }
}
