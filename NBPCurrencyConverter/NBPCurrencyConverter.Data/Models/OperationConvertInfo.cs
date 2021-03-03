using System;
using System.Collections.Generic;
using System.Text;

namespace NBPCurrencyConverter.Data.Models
{
    public class OperationConvertInfo : OperationInfo
    {
        public decimal Amount { get; set; }
        public decimal Result { get; set; }

        public string CurrencyCodeFrom { get; set; }
        public string CurrencyCodeTo { get; set; }

        public decimal CurrencyMidFrom { get; set; }
        public decimal CurrencyMidTo { get; set; }
    }
}
