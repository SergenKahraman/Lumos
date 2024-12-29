using Lumos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Lumos.Data.Context
{
    public class LumosDbContext : DbContext
    {
        public LumosDbContext()
        {
        }

        public LumosDbContext(DbContextOptions<LumosDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false, true).Build()
                .GetConnectionString("Local");
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.EnableDynamicJson();
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<InverterData> InverterDatas { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceTagMapping> DeviceTagMappings { get; set; }
    }
}