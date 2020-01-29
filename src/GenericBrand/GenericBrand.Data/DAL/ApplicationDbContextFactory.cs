using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GenericBrand.Data.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var variables = new EnvironmentInformation();
            var connectionString = new ConnectionStringBuilder().CreateConnectionString(variables.GetBasePath(), variables.GetEnvironmentName());

            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}
