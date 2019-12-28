using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
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
                await user.GetOrCreateDMChannelAsync();
            await user.SendMessageAsync($"You were banned from \"{Context.Guild.Name}\" by **{user.Username}**" + 
                                        "\nReason: {reason}");

            var eb = new EmbedBuilder()
                .WithTitle("{user.Mention} has been banned.")
                .AddField("Moderator:", Context.User.Username)
                .AddField("Reason", reason)
                .WithColor(Color.Blue)
                .WithCurrentTimestamp();

            await Context.Guild.AddBanAsync(user);
            await ReplyAsync(embed:eb.Build());
                
            }

            else
            {
                var band = new EmbedBuilder();

                band.WithTitle("Command: .ban");
                band.WithDescription("**Description:** Ban a member\n**Usage:** .ban [user] [reason]\n**Example:** .ban @yolo retard");
                band.WithColor(Color.Blue);

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
                var band = new EmbedBuilder();

                band.WithTitle("Command: .softban");
                band.WithDescription("**Description:** Bans a user then removes the ban.\n**Usage:** .ban [user] [reason]\n**Example:** .ban @yolo retard");
                band.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, band.Build());
            }
        }

        [Command("unban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Unbanuser(ulong userID = 0)
        {
            if (userID != 0)
            {
                await Context.Guild.RemoveBanAsync(userID);

                await Context.Channel.SendMessageAsync($"User has been unbanned!");
            }

            else
            {
                var band = new EmbedBuilder();

                band.WithTitle("Command: .unban");
                band.WithDescription("**Description:** Unbans a user from the server.\n**Usage:** .unban [userID]\n**Example:** .unban 312511213986906112");
                band.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, band.Build());
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
                await user.GetOrCreateDMChannelAsync();
                await user.SendMessageAsync($"You have been kicked from {Context.Guild.Name}, {reason}");

                await user.KickAsync(reason);

                var kickdmembed2 = new EmbedBuilder();

                kickdmembed2.Build();
                kickdmembed2.WithTitle($"{user} has been kicked");
                kickdmembed2.AddField($"Moderator:", Context.User.Username);
                kickdmembed2.AddField($"Reason:", reason);
                kickdmembed2.WithCurrentTimestamp();
                kickdmembed2.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, kickdmembed2.Build());

            }
            else
            {
                var band2 = new EmbedBuilder();

                band2.WithTitle("Command: .kick");
                band2.WithDescription("**Description:** Kick a member\n**Usage:** .kick [user] [reason]\n**Example:** .kick @yolo retard");
                band2.WithColor(Color.Blue);

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
                band4.WithColor(Color.Blue);

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

            }

            else
            {
                var band5 = new EmbedBuilder();

                band5.WithTitle("Command: .unmute");
                band5.WithDescription("**Description:** Unmutes a muted user\n**Usage:** .unmute [user]\n**Example:** .unmute @yolo");
                band5.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, band5.Build());

            }

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
                band3.WithColor(Color.Blue);


                await Context.Channel.SendMessageAsync("", false, band3.Build());
                
            }
           
        }

        [Command("nickname")]
        [Alias("nick")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        [RequireBotPermission(GuildPermission.ManageNicknames)]
        public async Task SetNicknameAsync([RequireUserHierarchy][RequireBotHierarchy]SocketGuildUser user, [Remainder]string nickname = null)
        {
            if (nickname != null)
            {
                if (nickname.Length! < 33)
                {
                    await user.ModifyAsync(x => x.Nickname = nickname);
                    await ReplyAsync($"Done! {user.Username}'s nickname has been updated.");
                }
                else
                {
                    await ReplyAsync("Nicknames may not exceed 32 characters. Please shorten it and try again.");
                }
            }

            else
            {
                var band3 = new EmbedBuilder();

                band3.WithTitle("Command: .nick");
                band3.WithDescription("**Description:** Changes the nickname of a user\n**Usage:** .nick [user] [nickname]\n**Example:** .nick @yolo pedoswag");
                band3.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, band3.Build());
            }
        }

    }
}