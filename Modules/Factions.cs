using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Cudi.Modules
{
    public class Factions : ModuleBase<ShardedCommandContext>
    {
        [Command("ping")]
        public async Task TestAsync()
        {
            await ReplyAsync("Pong! :ping_pong:");
        }
    }
}