using Lumos.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;

namespace Lumos.Producer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile($"appsettings.json", true);
            }).ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IMQTTProducer, MQTTProducer>();
                services.AddSingleton<MqttFactory>();
                services.AddScoped<DbContext, LumosDbContext>();
                services.AddDbContext<LumosDbContext>(options =>
                {
                    options.UseNpgsql(hostContext.Configuration.GetConnectionString("Local"));
                });
            }).Build();
            await host.StartAsync();
            await host.Services.GetRequiredService<IMQTTProducer>().Produce(host.Services); //start process
            await host.WaitForShutdownAsync();
        }
    }
}