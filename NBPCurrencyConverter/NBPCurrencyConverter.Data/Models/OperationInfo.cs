using System;
using System.Collections.Generic;
using System.Text;

namespace NBPCurrencyConverter.Data.Models
{
    public class OperationInfo
    {
        public int Id { get; set; }
        public string OperationTitle { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
