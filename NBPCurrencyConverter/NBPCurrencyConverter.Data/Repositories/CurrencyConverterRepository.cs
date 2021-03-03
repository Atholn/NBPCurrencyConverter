using NBPCurrencyConverter.Data.Models;
using NBPCurrencyConverter.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NBPCurrencyConverter.Data.Repositories
{
    public class CurrencyConverterRepository : ICurrencyConverterRepository
    {
        private readonly BaseContext _context;
        public CurrencyConverterRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task AddOperationConvertInfoAsync(OperationConvertInfo operationConvertInfo)
        {
            await _context.OperationsConvertInfo.AddAsync(operationConvertInfo);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task AddOperationCurrencyRetrievesInfoAsync(OperationCurrencyRetrievesInfo operationCurrencyRetrievesInfo)
        {          
            await _context.OperationsRetrivesCurrencyInfo.AddAsync(operationCurrencyRetrievesInfo);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
