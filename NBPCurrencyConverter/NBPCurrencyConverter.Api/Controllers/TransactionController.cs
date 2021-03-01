using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBPCurrencyConverter.Core.ModelsDTO;
using NBPCurrencyConverter.Core.Services.Interfaces;
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

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TransactionDto transactionDto)
        {
            var result = await _currencyConverterService.CurrencyRate(transactionDto);
            return Ok(result); //: BadRequest(result.ErrorMessage);
        }
    }
}
