using Microsoft.EntityFrameworkCore;
using NBPCurrencyConverter.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBPCurrencyConverter.Data
{
    public class BaseContext : DbContext
    {
        public DbSet<OperationInfo> OperationsInfo { get; set; }

        public BaseContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }     
    }
}
