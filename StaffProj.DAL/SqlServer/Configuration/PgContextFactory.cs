using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProj.DAL.SqlServer.Configuration
{
    public class PgContextFactory : IDesignTimeDbContextFactory<PgDbContext>
    {
        public PgDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<PgDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new PgDbContext(optionsBuilder.Options);
        }
    }
}
