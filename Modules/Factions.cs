using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Cudi.Modules
{
    public class Factions : ModuleBase<ShardedCommandContext>
    {
        private readonly string ftopPath = "Resources/ftop.txt";
        
        [Command("ftop")]
        public async Task GetFtopAsync()
        {
            IEnumerable<string> lines = File.ReadAllLines(ftopPath).TakeLast(10);
            var lastModify = (DateTime.Now - File.GetLastWriteTime(ftopPath)).TotalMinutes;

            var eb = new EmbedBuilder()
                .WithTitle($"Faction Top")
                .WithDescription(string.Join('\n', lines))
                .WithCurrentTimestamp()
                .WithColor(Color.Blue)
                .WithFooter($"Last updated {String.Format("{0:n0}", lastModify)} minutes ago");
            await ReplyAsync(embed:eb.Build());
        }
    }
}