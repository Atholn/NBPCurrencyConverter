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
        public CurrencyConverterService(ICurrencyConverterRepository currencyConverterRepository)
        {
            _currencyConverterRepository = currencyConverterRepository;
        }
        public async Task<decimal> CurrencyConverterAsync(TransactionDto transactionDto)
        {
            var rates = await GetListCurrencyAsync();

            transactionDto.CurrencyFrom = transactionDto.CurrencyFrom.ToUpper();
            transactionDto.CurrencyTo = transactionDto.CurrencyTo.ToUpper();

            OperationInfo operationInfo = new OperationInfo()
            {
                Amount = transactionDto.Amount,
                CurrencyCodeFrom = transactionDto.CurrencyFrom,
                CurrencyCodeTo = transactionDto.CurrencyTo,
                OperationDate = DateTime.Now,
                OperationTitle = "Currency convert",
            };

            //if (transactionDto.CurrencyFrom == "PLN" && transactionDto.CurrencyTo == "PLN")
            //{
            //    operationInfo.Result = transactionDto.Amount;

            //    operationInfo.CurrencyMidFrom = 1;
            //    operationInfo.CurrencyMidTo = 1;
            //}
            //else if (transactionDto.CurrencyFrom == "PLN")
            //{
            //    operationInfo.Result = transactionDto.Amount /
            //    rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyTo).Mid;

            //    operationInfo.CurrencyMidFrom = 1;
            //    operationInfo.CurrencyMidTo = rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyTo).Mid;
            //}
            //else if (transactionDto.CurrencyTo == "PLN")
            //{
            //    operationInfo.Result = transactionDto.Amount *
            //    rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyFrom).Mid;

            //    operationInfo.CurrencyMidFrom = rates.Where(x => x.Code == transactionDto.CurrencyFrom).Select(x => x.Mid).Single();
            //    operationInfo.CurrencyMidTo = 1;
            //}
            //else
            {
                operationInfo.Result = transactionDto.Amount *
                rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyFrom).Mid/
                rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyTo).Mid;

                operationInfo.CurrencyMidFrom = rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyFrom).Mid;
                operationInfo.CurrencyMidTo = rates.FirstOrDefault(x => x.Code == transactionDto.CurrencyTo).Mid;
            }

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
            ICollection<Rate> rates = null;
            foreach (var item in content) { rates = item.Rates; };

            rates.Add(new Rate { Code = "PLN", Mid = 1.0000m, Currency = "Polski złoty" });
            return rates;
        }
    }
}
