using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Cudi.Profiles;

namespace Cudi.Main
{
    public class EventHandler
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        
        private readonly DiscordShardedClient _client;

        public EventHandler(DiscordShardedClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
            _client.Log += Log;
            _client.MessageReceived += OnMessageReceived;
            _client.UserJoined += OnUserJoined;
            _client.JoinedGuild += OnJoinedGuild;
        }

        private async Task Log(LogMessage message)
        {
            Console.WriteLine(message);
        }
        
        private async Task OnMessageReceived(SocketMessage message)
        {
            if (message is IDMChannel || 
                message is ISystemMessage) return;
            
            UserProfiles.Get(message.Author as SocketGuildUser);
            UserProfiles.Save();
        }

        private async Task OnUserJoined(SocketGuildUser user)
        {
            UserProfiles.Get(user);
            UserProfiles.Save();
        }

        private async Task OnJoinedGuild(SocketGuild guild)
        {
            GuildProfiles.Get(guild);
            GuildProfiles.Save();
        }
    }
}