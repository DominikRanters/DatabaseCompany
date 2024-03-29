﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using Microsoft.Extensions.Options;

namespace CompanyAPI.Helper
{
    public class DbContext : IDbContext
    {
        private readonly DbSettings _settings;

        public DbContext(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<IDbConnection> GetConnection()
        {
            var con =  new SqlConnection(_settings.Connection);
            await con.OpenAsync();
            return con;
        }
    }
}
