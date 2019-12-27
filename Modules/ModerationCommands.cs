using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using WindowBot.Resources;
using WindowBot.Accounts.Guild;
using System.Linq;

namespace WindowBot.Modules
{
    public class ModerationCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Ban")]
        [Alias("ban")]
        [Summary("Bans a user from the server.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user = null, [Remainder]string reason = "no reason provided")
        {
            if (user != null)
            {
                await user.Guild.AddBanAsync(user);
                await user.GetOrCreateDMChannelAsync();
                await user.SendMessageAsync($"You have been banned from {Context.Guild.Name}, {reason}");

                var banembed2 = new EmbedBuilder();

                banembed2.WithTitle($"{user} has been banned.");
                banembed2.AddField($"Moderator:", Context.User.Username + Context.User.Discriminator);
                banembed2.AddField($"Reason:", reason);
                banembed2.WithCurrentTimestamp();
                banembed2.WithColor(Color.Red);


                await user.Guild.AddBanAsync(user);

                await Context.Channel.SendMessageAsync("", false, banembed2.Build());
                
            }

            else
            {
                var band = new EmbedBuilder();

                band.WithTitle("Command: .ban");
                band.WithDescription("**Description:** Ban a member\n**Usage:** .ban [user] [reason]\n**Example:** .ban @yolo retard");
                

                await Context.Channel.SendMessageAsync("", false, band.Build());
            }

        }

        [Command("Softban")]
        [Alias("softban")]
        [Summary("Softbans a user from the server.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task SoftBanUser(IGuildUser user = null, [Remainder]string reason = "no reason provided")
        {
            if (user != null)
            {
                await user.Guild.AddBanAsync(user, 7, reason);
                await user.Guild.RemoveBanAsync(user);
                await user.GetOrCreateDMChannelAsync();
                await user.SendMessageAsync($"You have been softbanned from {Context.Guild.Name}, {reason}");

                var softbanembed2 = new EmbedBuilder();
                softbanembed2.Build();
                softbanembed2.WithTitle($"{user} has been softbanned.");
                softbanembed2.AddField($"Moderator:", Context.User.Username + Context.User.Discriminator);
                softbanembed2.AddField($"Reason:", reason);
                softbanembed2.WithCurrentTimestamp();
                softbanembed2.WithColor(240, 225, 0);

                await Context.Channel.SendMessageAsync("", false, softbanembed2.Build());
            }
            else
            {
                
            }
        }

        [Command("Kick")]
        [Alias("kick")]
        [Summary("Kicks a user from the server.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user = null, [Remainder]string reason = "no reason provided")
        {
            if (user != null)
            {
                await user.KickAsync(reason);
                await user.GetOrCreateDMChannelAsync();
                await user.SendMessageAsync($"You have been kicked from {Context.Guild.Name}, {reason}");

                var kickdmembed2 = new EmbedBuilder();
                kickdmembed2.Build();
                kickdmembed2.WithTitle($"{user} has been kicked");
                kickdmembed2.AddField($"Moderator:", Context.User.Username + Context.User.Discriminator);
                kickdmembed2.AddField($"Reason:", reason);
                kickdmembed2.WithCurrentTimestamp();
                kickdmembed2.WithColor(Color.LightOrange);
                await Context.Channel.SendMessageAsync("", false, kickdmembed2.Build());
                await user.SendMessageAsync("", false, kickdmembed2.Build());
                await user.KickAsync(reason);
            }
            else
            {
                var band2 = new EmbedBuilder();

                band2.WithTitle("Command: .kick");
                band2.WithDescription("**Description:** Kick a member\n**Usage:** .kick [user] [reason]\n**Example:** .kick @yolo retard");


                await Context.Channel.SendMessageAsync("", false, band2.Build());
            }
        }

        [Command("Mute")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task mute(IGuildUser user = null)
        {
            if (user != null)
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
                await (user as IGuildUser).AddRoleAsync(role);

                await Context.Channel.SendMessageAsync("User has been muted.", false);
            }
           
            else
            {
                var band4 = new EmbedBuilder();

                band4.WithTitle("Command: .mute");
                band4.WithDescription("**Description:** Mutes a user\n**Usage:** .mute [user]\n**Example:** .mute @yolo");

                await Context.Channel.SendMessageAsync("", false, band4.Build());
            }
        }


        [Command("Unmute")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task mute2(IGuildUser user = null)
        {

            if (user != null)
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
                await (user as IGuildUser).RemoveRoleAsync(role);

                await Context.Channel.SendMessageAsync("User has been unmuted.", false);
                var band5 = new EmbedBuilder();

                band5.WithTitle("Command: .unmute");
                band5.WithDescription("**Description:** Unmutes a muted user\n**Usage:** .unmute [user]\n**Example:** .unmute @yolo");

                await Context.Channel.SendMessageAsync("", false, band5.Build());
            }

            else
            {
                var band5 = new EmbedBuilder();

                band5.WithTitle("Command: .unmute");
                band5.WithDescription("**Description:** Unmutes a muted user\n**Usage:** .unmute [user]\n**Example:** .unmute @yolo");

                await Context.Channel.SendMessageAsync("", false, band5.Build());

            }

        }

        [Command("Warn")]
        [Summary("Warns a user.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task WarnUser(IGuildUser user, string reason)
        {
            


        }

        [Command("Purge")]
        [Alias("purge", "prune", "clear")]
        [Summary("Deletes the speficed amamount of messages.")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ClearMessages(int amount = 0)
        {
            if (amount != 0)
            {
                await Context.Message.DeleteAsync();
                var messages = await Context.Channel.GetMessagesAsync(amount, CacheMode.AllowDownload)?.FlattenAsync();

                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                const int delay = 2000;

                var m = await ReplyAsync($"Purge Complete. **This message will be deleted in 2 seconds**");
                await Task.Delay(delay);
                await m.DeleteAsync();
            }
                
            else
            {
                var band3 = new EmbedBuilder();

                band3.WithTitle("Command: .purge");
                band3.WithDescription("**Description:** Purge a certain amount of messages\n**Usage:** .purge [amount]\n**Example:** .purge 100");


                await Context.Channel.SendMessageAsync("", false, band3.Build());
                
            }
           
        }

        [Command("Setnick")]
        [Alias("setnick", "changenick")]
        [Summary("Change another user's nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        [RequireBotPermission(GuildPermission.ManageNicknames)]
        public async Task Nick(SocketGuildUser user, [Remainder]string name)
        {
            await user.ModifyAsync(x => x.Nickname = name);
            await Context.Channel.SendMessageAsync($"{user}'s nick has been changed.", false);
        }

        [Command("Announce")]
        [Alias("announce", "a")]
        [Summary("Sends the specified text into an announcement channel with an embed.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AnnouncementAsync(string tag, [Remainder] string text)
        {
            if (tag != null)
            {
                var announce = new EmbedBuilder();
                var RandomizeColor = new Random().Next(Constants.colorsArray.Length);

                announce.WithDescription(text);
                announce.WithFooter($"Announcement created by {Context.User.Username + Context.User.Discriminator}");
                announce.WithColor(Constants.colorsArray[RandomizeColor]);

                await Context.Channel.SendMessageAsync($"{tag}");
                await Context.Channel.SendMessageAsync("", false, announce.Build());
                await Context.Message.DeleteAsync();
            }
            else
            {
                var announce = new EmbedBuilder();
                var RandomizeColor = new Random().Next(Constants.colorsArray.Length);

                announce.WithDescription(text);
                announce.WithFooter($"Announcement created by {Context.User.Username + Context.User.Discriminator}");
                announce.WithColor(Constants.colorsArray[RandomizeColor]);

                await Context.Channel.SendMessageAsync("", false, announce.Build());
                await Context.Message.DeleteAsync();
            }
        }
    }
}