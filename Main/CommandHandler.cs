using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Cudi.Profiles;

namespace Cudi.Main
{
    public class CommandHandler
    {
        private readonly DiscordShardedClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _services;

        public CommandHandler(DiscordShardedClient client, CommandService command, IServiceProvider serv)
        {
            _client = client;
            _command = command;
            _services = serv;
        }

        public async Task Initialize()
        {
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage message)
        {
            if (message is IDMChannel || 
                message is ISystemMessage || 
                message.Author.IsWebhook ||
                message.Author.IsBot) return;

            var msg = message as SocketUserMessage;
            var context = new ShardedCommandContext(_client, msg);
            int argPos = 0;

            var guild = GuildProfiles.Get(context.Guild);

            if (msg.HasMentionPrefix(_client.CurrentUser, ref argPos) 
                || msg.HasStringPrefix(guild.Prefix, ref argPos))
            {
                IResult result = await _command.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    // error handling
                }
            }
        }
    }
}