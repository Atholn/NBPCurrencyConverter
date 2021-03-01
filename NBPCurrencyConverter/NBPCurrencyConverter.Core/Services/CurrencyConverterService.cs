using NBPCurrencyConverter.Core.ModelsDTO;
using NBPCurrencyConverter.Core.Services.Interfaces;
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
        public async Task<decimal> CurrencyRate(TransactionDto transactionDto)
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
            ICollection<Rates> rates = null;
            foreach (var item in content) { rates = item.Rates;};

            return Decimal.Round(
                transactionDto.Amount *
                rates.Where(x => x.Code == transactionDto.CurrencyFrom).Select(x => x.Mid).Single()/
                rates.Where(x => x.Code == transactionDto.CurrencyTo).Select(x => x.Mid).Single(),
                2);
        }

    }
}
