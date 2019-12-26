using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Cudi.Main;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = Cudi.Main.EventHandler;

namespace Cudi
{
    public class Program
    {
        public DiscordShardedClient _client;
        public IServiceProvider _services;
        public Config _config;

        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            // Initialize instance of config, later add to DI
            _config = new Config();
            
            // Check token
            if (string.IsNullOrEmpty(_config.Settings.Token))
            {
                Console.WriteLine("Token missing, please restart the application with a token configured.");
            }

            // Create client
            _client = CreateClientAsync();

            // Set-up DI
            _services = ConfigureServices();
            _services.GetRequiredService<EventHandler>().Initialize();
            await _services.GetRequiredService<CommandHandler>().Initialize();
            
            // Start client
            await _client.LoginAsync(TokenType.Bot, _config.Settings.Token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        public DiscordShardedClient CreateClientAsync()
        {
            return new DiscordShardedClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 50,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                ExclusiveBulkDelete = true
            });
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_config)
                .AddSingleton<CommandHandler>()
                .AddSingleton<CommandService>()
                .AddSingleton<EventHandler>()
            .BuildServiceProvider();
        }
    }
}
