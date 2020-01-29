using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericBrand.Data.DAL
{
    internal class ConnectionStringBuilder
    {
        public virtual string CreateConnectionString(string basePath, string environmentName)
        {
            string databaseConnectionString = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddJsonFile("local.settings.json", true)
                .AddEnvironmentVariables()
                .Build()
                .GetConnectionString("AppDbConnection");

            return databaseConnectionString;
        }
    }
}
