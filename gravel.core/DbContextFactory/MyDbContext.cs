using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Data.Common;

namespace gravel.core.DbContextFactory
{
    public class MyDbContext : DapperContext
    {
 
        public MyDbContext(string connString)
            : base(connString)
        {
           
        }

    }
}
