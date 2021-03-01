using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPCurrencyConverter.Core.ModelsDTO
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
    }
}
