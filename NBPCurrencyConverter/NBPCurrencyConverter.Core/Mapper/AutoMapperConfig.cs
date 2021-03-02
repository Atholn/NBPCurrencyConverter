using AutoMapper;
using NBPCurrencyConverter.Core.ModelsDTO;
using NBPCurrencyConverter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPCurrencyConverter.Core.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TransactionDto, OperationInfo>();
        }).CreateMapper();
    }
}
