using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBPCurrencyConverter.Core.ModelsDTO;
using NBPCurrencyConverter.Core.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBPCurrencyConverter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ICurrencyConverterService _currencyConverterService;

        public TransactionController(ICurrencyConverterService currencyConverterService)
        {
            _currencyConverterService = currencyConverterService;
        }

        [SwaggerOperation(Summary = "Convert rate")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TransactionDto transactionDto)
        {
            var result = await _currencyConverterService.CurrencyConverterAsync(transactionDto);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Retrieves code currency")]
        [HttpGet("Code")]
        public async Task<ActionResult> GetCodeCurrency()
        {
            var result = await _currencyConverterService.GetListCurrencyCodesAsync();
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Retrieves currency rates")]
        [HttpGet("Currency")]
        public async Task<ActionResult> GetCurrencyOfRates()
        {
            var result = await _currencyConverterService.GetListCurrencyRatesAsync();

            return Ok(result);
        }
    }
}
