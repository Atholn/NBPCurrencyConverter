using NBPCurrencyConverter.Core.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPCurrencyConverter.Core.Services.Interfaces
{
    public interface ICurrencyConverterService
    {
        Task<decimal> CurrencyConverterAsync(TransactionDto transaction);
        Task<ICollection<Rate>> GetListCurrencyRatesAsync();
        Task<ICollection<string>> GetListCurrencyCodesAsync();
        
        Task<ICollection<Rate>> GetListCurrencyAsync();
    }
}
