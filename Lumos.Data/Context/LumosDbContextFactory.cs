using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Lumos.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class LumosDbContextFactory : IDesignTimeDbContextFactory<LumosDbContext>
    {
        public LumosDbContext CreateDbContext(string[] args)
        {
            var connectionString = new ConfigurationBuilder()
                            .SetBasePath(System.IO.Path.Combine(Directory.GetCurrentDirectory()))
                            .AddJsonFile("appsettings.json", false, true).Build()
                            .GetConnectionString("Local");
            var builder = new DbContextOptionsBuilder<LumosDbContext>();
            builder.UseNpgsql(connectionString, x => x.MigrationsHistoryTable("__MigrationsHistory", "public"));
            return new LumosDbContext(builder.Options);
        }
    }
}