using AutoMapper;
using NBPCurrencyConverter.Core.ModelsDTO;
using NBPCurrencyConverter.Core.Services.Interfaces;
using NBPCurrencyConverter.Data.Models;
using NBPCurrencyConverter.Data.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBPCurrencyConverter.Core.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private const string url = "http://api.nbp.pl/api/exchangerates/tables/a/?format=json";
        private readonly ICurrencyConverterRepository _currencyConverterRepository;
        private readonly IMapper _mapper;
        public CurrencyConverterService(ICurrencyConverterRepository currencyConverterRepository, IMapper mapper)
        {
            _currencyConverterRepository = currencyConverterRepository;
            _mapper = mapper;
        }

        public async Task<decimal> CurrencyConverterAsync(TransactionDto transactionDto)
        {
            var rates = await GetListCurrencyAsync();

            transactionDto.CurrencyCodeFrom = transactionDto.CurrencyCodeFrom.ToUpper();
            transactionDto.CurrencyCodeTo = transactionDto.CurrencyCodeTo.ToUpper();


            var operationInfo = _mapper.Map<OperationInfo>(transactionDto);
            operationInfo.OperationDate = DateTime.Now;
            operationInfo.OperationTitle = "Currency convert";
            //OperationInfo operationInfo = new OperationInfo()
            //{
            //    Amount = transactionDto.Amount,
            //    CurrencyCodeFrom = transactionDto.CurrencyCodeFrom,
            //    CurrencyCodeTo = transactionDto.CurrencyCodeTo,
            //    OperationDate = DateTime.Now,
            //    OperationTitle = "Currency convert",
            //};

            operationInfo.Result = transactionDto.Amount *
            rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyCodeFrom).Mid /
            rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyCodeTo).Mid;

            operationInfo.CurrencyMidFrom = rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyCodeFrom).Mid;

            await _currencyConverterRepository.AddOperationInfoAsync(operationInfo);

            return Decimal.Round(operationInfo.Result, 2);
        }

        public async Task<ICollection<Rate>> GetListCurrencyOfRatesAsync()
        {
            var rates = await GetListCurrencyAsync();
            return rates;
        }

        public async Task<ICollection<string>> GetListCurrencyCodeAsync()
        {
            var rates = await GetListCurrencyAsync();
            return rates.Select(x => x.Code).ToList();
        }

        public async Task<ICollection<Rate>> GetListCurrencyAsync()
        {
            WebRequest requestObjGet = WebRequest.Create(url);
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

            string json = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                json = sr.ReadToEnd();
                sr.Close();
            }

            var content = JsonConvert.DeserializeObject<ICollection<TableRates>>(json);
            List<Rate> rates = null;
            foreach (var item in content)
                rates = item.Rates;

            await Task.Run(() =>
            {
                rates.Add(new Rate { Code = "PLN", Mid = 1.0000m, Currency = "Polski złoty" });
                
            });
            return rates;

        }
    }
}
