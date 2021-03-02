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
        Task<decimal> CurrencyConverterAsync(TransactionDto transactionDto);
        Task<ICollection<Rate>> GetListCurrencyOfRatesAsync();
        Task<ICollection<string>> GetListCurrencyCodeAsync();
        
        Task<ICollection<Rate>> GetListCurrencyAsync();
    }
}
